namespace a3innuva.TAA.Migration.SDK.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using a3innuva.TAA.Migration.SDK.Interfaces;

    public abstract class Validation<TEntity> : IValidation<TEntity> where TEntity : IMigrationEntity
    {
        private readonly List<Func<TEntity, IValidationResult>> actions;

        protected Validation()
        {
            this.actions = new List<Func<TEntity, IValidationResult>>();
            this.SetupValidations();
        }

        public virtual IEnumerable<IValidationResult> Validate(TEntity entity)
        {
            List<IValidationResult> errors = new List<IValidationResult>();

            this.actions.ForEach(x =>
            {
                var error = x.Invoke(entity);

                if (!error.IsValid)
                    errors.Add(error);
            });

            return errors;
        }

        protected abstract void SetupValidations();

        protected void CreateRule(Func<TEntity, bool> rule, string code)
        {
            Func<TEntity, ValidationResult> action = new Func<TEntity, ValidationResult>(x =>
            {
                bool isValid = rule.Invoke(x);

                return new ValidationResult()
                {
                    Line = x.Line,
                    IsValid = isValid,
                    Code = code
                };
            });

            this.actions.Add(action);
        }

        protected bool Validate(DateTime date)
        {
            return date != DateTime.MinValue && date <= new DateTime(2100,12,31);
        }

        protected bool Validate(DateTime? date)
        {
            if (date == null)
                return true;

            return this.Validate(date.Value);
        }

        protected bool Validate(Guid id)
        {
            return id != Guid.Empty;
        }


        protected bool ValidateNullable(string text, int length)
        {
            if (String.IsNullOrEmpty(text?.Trim()))
                return true;

            if (text.Length > length)
                return false;

            return true;
        }

        protected bool Validate(string text, int maxlength)
        {
            if (String.IsNullOrEmpty(text?.Trim()))
                return false;

            if (text.Length > maxlength)
                return false;


            return true;
        }

        protected bool Validate(string text)
        {
            return !String.IsNullOrEmpty(text?.Trim());
        }

        protected bool Validate(decimal input)
        {
            return input != 0;
        }

        protected string ReplaceInMessage(string message, params string[] list)
        {
            var keys = new List<(int,string)>();
            int i = 0;
            foreach (var item in list)
            {
                keys.Add((i,$"#Key{i}#"));
                i++;
            }

            string replace = message;

            keys.ForEach(x =>
            {
                replace = replace.Replace(x.Item2, list[x.Item1]);
            });

            return replace;
        }

        [ExcludeFromCodeCoverage]
        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
                return;

            this.actions?.Clear();
        }

        [ExcludeFromCodeCoverage]
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
