namespace a3innuva.TAA.Migration.SDK.Implementations
{
    using System.Collections.Generic;
    using System.Linq;
    using a3innuva.TAA.Migration.SDK.Interfaces;

    public class JournalValidation : Validation<IJournal>
    {
        private readonly IValidation<IJournalLine> lineValidation;

        public JournalValidation()
        {
            this.lineValidation = new JournalLineValidation();
        }

        protected override void SetupValidations()
        {
            this.CreateRule(x => this.Validate(x.Id), "Id");
            this.CreateRule(x => this.Validate(x.Date), this.ReplaceInMessage(ValidationMessages.Mandatory, "'Fecha de asiento'"));
            this.CreateRule(x => this.Validate(x.Number), this.ReplaceInMessage(ValidationMessages.Mandatory, "'Número de asiento'"));
            this.CreateRule(x => this.Validate(x.Number, 60), this.ReplaceInMessage(ValidationMessages.InvalidLength, "'Número de asiento'"));
            this.CreateRule(x => this.ValidateNullable(x.Document, 60), this.ReplaceInMessage(ValidationMessages.InvalidLength, "'Documento'"));
            this.CreateRule(x => this.Validate(x.Source), this.ReplaceInMessage(ValidationMessages.Mandatory, "'Origen'"));
        }

        public override IEnumerable<IValidationResult> Validate(IJournal entity)
        {
            List<IValidationResult> errors = new List<IValidationResult>();

            foreach (var item in entity.Lines)
            {
                var result = lineValidation.Validate(item);

                var resultErrors = result.Where(x => x.IsValid == false);
                errors.AddRange(resultErrors);
            }

            var entityErrors = base.Validate(entity);

            errors.AddRange(entityErrors);
            return errors;
        }
    }
}
