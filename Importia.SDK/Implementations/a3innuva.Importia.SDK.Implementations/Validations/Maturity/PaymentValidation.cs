namespace a3innuva.TAA.Migration.SDK.Implementations
{
    using a3innuva.TAA.Migration.SDK.Interfaces;
    using System.Text.RegularExpressions;

    public class PaymentValidation : Validation<IPayment>
    {
        private readonly Regex accountCodeFormat;

        public PaymentValidation()
        {
            this.accountCodeFormat = new Regex(@"^[1-9]{1}[0-9]*$", RegexOptions.Compiled);
        }

        protected override void SetupValidations()
        {
            this.CreateRule(x => this.Validate(x.Id), "Id");

            this.CreateRule(x => this.Validate(x.Amount), this.ReplaceInMessage(ValidationMessages.Mandatory, "'Importe'"));

            this.CreateRule(x => this.Validate(x.Date), this.ReplaceInMessage(ValidationMessages.Mandatory, "'Fecha'"));

            this.CreateRule(x => x.BankAccount == null || x.BankAccount.Length <= 20, this.ReplaceInMessage(ValidationMessages.InvalidLength, "'Cuenta bancaria'"));
            this.CreateRule(x => x.BankAccount == null || this.accountCodeFormat.IsMatch(x.BankAccount), this.ReplaceInMessage(ValidationMessages.InvalidFormat, "'Cuenta bancaria'"));

            this.CreateRule(x => x.BankAccount == null || this.Validate(x.BankAccountDescription), this.ReplaceInMessage(ValidationMessages.Mandatory, "'Descripción de cuenta bancaria'"));
            this.CreateRule(x => x.BankAccount == null || this.Validate(x.BankAccountDescription, 255), this.ReplaceInMessage(ValidationMessages.InvalidLength, "'Descripción de cuenta bancaria'"));

            this.CreateRule(x => this.ValidateAccountingCanBeAffected(x.AccountingAffect, x.Situation, x.BankAccount), this.ReplaceInMessage(ValidationMessages.InvalidValue, "'Efecto contable'"));
        }

        private bool ValidateAccountingCanBeAffected(bool accountingAffect, PaymentSituation situation, string bankAccount)
            => !(situation == PaymentSituation.Pending && accountingAffect) && !(accountingAffect && string.IsNullOrEmpty(bankAccount));
    }
}
