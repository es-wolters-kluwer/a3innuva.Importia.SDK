namespace a3innuva.TAA.Migration.SDK.Implementations
{
    using System.Text.RegularExpressions;
    using a3innuva.TAA.Migration.SDK.Interfaces;

    public class OutputInvoiceLineValidation : Validation<IOutputInvoiceLine>
    {
        private readonly Regex accountCodeFormat;
        private readonly Regex nifFormat;

        public OutputInvoiceLineValidation()
        {
            this.accountCodeFormat = new Regex(@"^[1-9]{1}[0-9]*$");
            this.nifFormat = new Regex(@"^[A-Z0-9]*$");
        }

        protected override void SetupValidations()
        {
            this.CreateRule(x => this.Validate(x.Id), "Id");
            this.CreateRule(x => this.Validate(x.BaseAmount), this.ReplaceInMessage(ValidationMessages.Mandatory, "'Base imponible'"));
            this.CreateRule(x => this.ValidateNullable(x.BaseAccount, 20), this.ReplaceInMessage(ValidationMessages.InvalidLength, "'Cuenta de la base'"));
            this.CreateRule(x => this.ValidateAccountFormat(x.BaseAccount), this.ReplaceInMessage(ValidationMessages.InvalidFormat, "'Cuenta de la base'"));

            this.CreateRule(x => this.Validate(x.Transaction), this.ReplaceInMessage(ValidationMessages.Mandatory, "'Operación'"));
            this.CreateRule(x => this.ValidateTransaction(x.Transaction), this.ReplaceInMessage("No es una operación valida"));

            this.CreateRule(x => this.ValidateWithHolding(x.WithHolding), this.ReplaceInMessage("No es una retención valida"));

            this.CreateRule(x => this.ValidatePercentage(x.WithHoldingPercentage), this.ReplaceInMessage(ValidationMessages.InvalidFormat, "'Porcentaje de retención'"));
        }
        
        private bool ValidateTransaction(string input)
        {
            if (string.IsNullOrEmpty(input))
                return false;

            var transactions = new Transactions();

            return transactions.ItExistForOutput(input);
        }

        private bool ValidateWithHolding(string input)
        {
            if (string.IsNullOrEmpty(input))
                return true;

            var winthHoldings = new WithHoldings();

            return winthHoldings.ItExistForOutput(input);
        }

        private bool ValidatePercentage(decimal ? input)
        {
            if (input == null)
                return true;

            return 0 <= input &&  input <= 100;
        }

        private bool ValidateAccountFormat(string input)
        {
            if (string.IsNullOrEmpty(input))
                return true;

            return this.accountCodeFormat.IsMatch(input);
        }
    }
}
