namespace a3innuva.TAA.Migration.SDK.Implementations
{
    using System.Text.RegularExpressions;
    using a3innuva.TAA.Migration.SDK.Interfaces;

    public class JournalLineValidation : Validation<IJournalLine>
    {
        private readonly Regex accountCodeFormat;

        public JournalLineValidation()
        {
            this.accountCodeFormat = new Regex(@"^[1-9]{1}[0-9]*$");
        }

        protected override void SetupValidations()
        {
            this.CreateRule(x => this.Validate(x.Id), "Id");

            this.CreateRule(x => this.Validate(x.Account), this.ReplaceInMessage(ValidationMessages.Mandatory, "'Cuenta'"));
            this.CreateRule(x => this.Validate(x.Account, 20), this.ReplaceInMessage(ValidationMessages.InvalidLength, "'Cuenta'"));
            this.CreateRule(x => this.accountCodeFormat.IsMatch(x.Account), this.ReplaceInMessage(ValidationMessages.InvalidFormat, "'Cuenta'"));

            this.CreateRule(x => this.Validate(x.AccountDescription), this.ReplaceInMessage(ValidationMessages.Mandatory, "'Descripción cuenta'"));
            this.CreateRule(x => this.Validate(x.AccountDescription, 255), this.ReplaceInMessage(ValidationMessages.InvalidLength, "'Descripción cuenta'"));

            this.CreateRule(x => this.ValidateNullable(x.Document, 60), this.ReplaceInMessage(ValidationMessages.InvalidLength, "'Documento'"));
            this.CreateRule(x => this.ValidateNullable(x.Concept, 255), this.ReplaceInMessage(ValidationMessages.InvalidLength, "'Concepto'"));

            this.CreateRule(x => x.HasValidAmount(), "Los importes no son correctos");

        }

    }
}
