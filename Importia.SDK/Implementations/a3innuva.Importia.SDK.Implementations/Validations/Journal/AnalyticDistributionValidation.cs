namespace a3innuva.TAA.Migration.SDK.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using a3innuva.TAA.Migration.SDK.Interfaces;
    using a3innuva.TAA.Migration.SDK.Interfaces.Entities.Journal;

    public class AnalyticDistributionValidation
    {
        private readonly List<Func<IAnalyticDistribution, IValidationResult>> actions;

        public AnalyticDistributionValidation()
        {
            this.actions = new List<Func<IAnalyticDistribution, IValidationResult>>();
            this.SetupValidations();
        }

        private void SetupValidations()
        {
            this.CreateRule(
                x => x.Percentage != 0,
                this.ReplaceInMessage(ValidationMessages.InvalidValue, "'Porcentaje de distribuci�n anal�tica'"));

            this.CreateRule(
                x => x.Details != null && x.Details.Any(),
                "Cada distribuci�n anal�tica debe tener al menos un detalle");

            this.CreateRule(
                x => this.ValidateDetails(x),
                "Los campos 'Level' y 'CostCenter' de los detalles de distribuci�n anal�tica son obligatorios");
        }

        public IEnumerable<IValidationResult> Validate(IAnalyticDistribution distribution, int line)
        {
            var errors = new List<IValidationResult>();

            this.actions.ForEach(action =>
            {
                var result = action.Invoke(distribution);
                result.Line = line;

                if (!result.IsValid)
                    errors.Add(result);
            });

            return errors;
        }

        private void CreateRule(Func<IAnalyticDistribution, bool> rule, string code)
        {
            this.actions.Add(distribution => new ValidationResult()
            {
                IsValid = rule.Invoke(distribution),
                Code = code
            });
        }

        private bool ValidateDetails(IAnalyticDistribution distribution)
        {
            if (distribution.Details == null)
                return false;

            return distribution.Details.All(detail =>
                !string.IsNullOrEmpty(detail.Level?.Trim()) &&
                !string.IsNullOrEmpty(detail.CostCenter?.Trim()));
        }

        private string ReplaceInMessage(string message, params string[] list)
        {
            var keys = new List<(int, string)>();
            int i = 0;
            foreach (var item in list)
            {
                keys.Add((i, $"#Key{i}#"));
                i++;
            }

            string replace = message;

            keys.ForEach(x =>
            {
                replace = replace.Replace(x.Item2, list[x.Item1]);
            });

            return replace;
        }
    }
}
