namespace a3innuva.TAA.Migration.SDK.Implementations
{
    using System;
    using System.Text.RegularExpressions;
    using a3innuva.TAA.Migration.SDK.Interfaces;

    public class PartnerValidation : Validation<IPartner>
    {
        private readonly Regex accountCodeFormat;
        public PartnerValidation()
        {
            this.accountCodeFormat = new Regex(@"^[1-9]{1}[0-9]*$", RegexOptions.Compiled, TimeSpan.FromSeconds(5));
        }

        protected override void SetupValidations()
        {
            this.CreateRule(x => this.Validate(x.Id), "Id");
            this.CreateRule(x => this.ValidateNullable(x.CounterPartAccountCode, 20), this.ReplaceInMessage(ValidationMessages.InvalidLength, "'Contrapartida'"));
            this.CreateRule(x => this.ValidateAccountFormat(x.CounterPartAccountCode), this.ReplaceInMessage(ValidationMessages.InvalidFormat, "'Contrapartida'"));
            this.CreateRule(x => x.MaturitiesAccountCode == null || x.MaturitiesAccountCode.Length <= 20, this.ReplaceInMessage(ValidationMessages.InvalidLength, "'Cuenta bancaria'"));
            this.CreateRule(x => x.MaturitiesAccountCode == null || this.accountCodeFormat.IsMatch(x.MaturitiesAccountCode), this.ReplaceInMessage(ValidationMessages.InvalidFormat, "'Cuenta bancaria'"));
            this.CreateRule(x => this.Validate(x.TransactionCode), this.ReplaceInMessage(ValidationMessages.Mandatory, "'Operación'"));
            this.CreateRule(x => this.ValidateTransaction(x.TransactionCode), this.ReplaceInMessage("No es una operación valida"));
            this.CreateRule(x => this.ValidateWithHolding(x.WithHoldingCode), this.ReplaceInMessage("No es una retención valida"));
		}
        
        private bool ValidateTransaction(string input)
        {
            if (string.IsNullOrEmpty(input))
                return false;

            return Transactions.ItExistForOutput(input);
        }

        private bool ValidateWithHolding(string input)
        {
            if (string.IsNullOrEmpty(input))
                return true;

            return WithHoldings.ItExistForOutput(input);
        }

        private bool ValidateAccountFormat(string input)
        {
            if (string.IsNullOrEmpty(input))
                return true;

            return this.accountCodeFormat.IsMatch(input);
        }
    }
}
