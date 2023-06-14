namespace a3innuva.TAA.Migration.SDK.Implementations
{
    using System.Text.RegularExpressions;
    using a3innuva.TAA.Migration.SDK.Interfaces;

    public class InputInvoiceLineValidation : Validation<IInputInvoiceLine>
    {
        private readonly Regex accountCodeFormat;
        public InputInvoiceLineValidation()
        {
            this.accountCodeFormat = new Regex(@"^[1-9]{1}[0-9]*$");
        }

        protected override void SetupValidations()
        {
            this.CreateRule(x => this.Validate(x.Id), "Id");
            this.CreateRule(x => this.ValidateNullable(x.CounterPart, 20), this.ReplaceInMessage(ValidationMessages.InvalidLength, "'Contrapartida'"));
            this.CreateRule(x => this.ValidateAccountFormat(x.CounterPart), this.ReplaceInMessage(ValidationMessages.InvalidFormat, "'Contrapartida'"));
            this.CreateRule(x => this.ValidateNullable(x.CounterPartDescription, 255), this.ReplaceInMessage(ValidationMessages.InvalidLength, "'Descripción contrapartida'"));
            this.CreateRule(x => this.Validate(x.Transaction), this.ReplaceInMessage(ValidationMessages.Mandatory, "'Operación'"));
            this.CreateRule(x => this.ValidateTransaction(x.Transaction), this.ReplaceInMessage("No es una operación valida"));
            this.CreateRule(x => this.ValidateWithHolding(x.WithHolding), this.ReplaceInMessage("No es una retención valida"));
            this.CreateRule(x => this.ValidatePercentage(x.WithHoldingPercentage), this.ReplaceInMessage(ValidationMessages.InvalidFormat, "'Porcentaje de retención'"));
        }

        private bool ValidateTransaction(string input)
        {
            if (string.IsNullOrEmpty(input))
                return false;

            return Transactions.ItExistForInput(input);
        }

        private bool ValidateWithHolding(string input)
        {
            if (string.IsNullOrEmpty(input))
                return true;

            return WithHoldings.ItExistForInput(input);
        }

        private bool ValidatePercentage(decimal ? input)
        {
            return input.ValidatePercentage();
        }

        private bool ValidateAccountFormat(string input)
        {
            if (string.IsNullOrEmpty(input))
                return true;

            return this.accountCodeFormat.IsMatch(input);
        }
    }
}
