namespace a3innuva.TAA.Migration.SDK.Implementations
{
    using System.Text.RegularExpressions;
    using a3innuva.TAA.Migration.SDK.Interfaces;


    public class AccountValidation : Validation<IAccount>
    {
        private readonly Regex accountCodeFormat;
        private readonly Regex nifFormat;
        private readonly Regex postalCodeFormat;

        public AccountValidation()
        {
            this.accountCodeFormat = new Regex(@"^[1-9]{1}[0-9]*$");
            this.nifFormat = new Regex(@"^[A-Z0-9]*$");
            this.postalCodeFormat = new Regex(@"^[0-9]{5}$");
        }

        protected override void SetupValidations()
        {
            this.CreateRule(x => this.Validate(x.Id), "Id");

            this.CreateRule(x => this.Validate(x.Code), this.ReplaceInMessage(ValidationMessages.Mandatory, "'Cuenta'"));
            this.CreateRule(x => this.Validate(x.Code, 20), this.ReplaceInMessage(ValidationMessages.InvalidLength, "'Cuenta'"));
            this.CreateRule(x => x.Code != null && this.accountCodeFormat.IsMatch(x.Code), this.ReplaceInMessage(ValidationMessages.InvalidFormat, "'Cuenta'"));

            this.CreateRule(x => this.Validate(x.Description), this.ReplaceInMessage(ValidationMessages.Mandatory, "'Descripción'"));
            this.CreateRule(x => this.Validate(x.Description, 255), this.ReplaceInMessage(ValidationMessages.InvalidLength, "'Descripción'"));

            this.CreateRule(x => this.ValidateNullable(x.VatNumber, 20), this.ReplaceInMessage(ValidationMessages.InvalidLength, "'NIF'"));
            this.CreateRule(x => this.ValidateVatNumber(x.VatNumber), this.ReplaceInMessage(ValidationMessages.InvalidFormat, "'NIF'"));

            this.CreateRule(x => this.ValidateNullable(x.Name, 255), this.ReplaceInMessage(ValidationMessages.InvalidLength, "'Nombre fiscal'"));

            this.CreateRule(x => this.ValidatePostalCode(x.PostalCode), this.ReplaceInMessage(ValidationMessages.InvalidFormat, "'Código postal'"));

            this.CreateRule(this.ValidateFiscalData, "Datos fiscales incompletos");

            this.CreateRule(this.ValidateIsNotLevel, "Un nivel de plan contable no admite datos fiscales");

            this.CreateRule(x => this.ValidateVatType(x.VatType), this.ReplaceInMessage(ValidationMessages.InvalidFormat, "'Tipo de documento'"));
            this.CreateRule(x => this.ValidateNullable(x.CountryCode, 2), this.ReplaceInMessage(ValidationMessages.InvalidLength, "'Código país'"));
        }

        private bool ValidateIsNotLevel(IAccount account)
        {
            if (account.Code == null || account.Code.Length > 5)
                return true;

            bool isNotLevel = !account.HasVatNumber();
            isNotLevel &= !account.HasName();
            isNotLevel &= !account.HasPostalCode();

            return isNotLevel;
        }

        private bool ValidateFiscalData(IAccount account)
        {
            if (account.Code == null || account.Code.Length < 6)
                return true;

            if (account.HasPostalCode())
                return account.HasName() && account.HasVatNumber();

            if (account.HasName())
                return account.HasVatNumber();

            if (account.HasVatNumber())
                return account.HasName();

            return true;
        }

        private bool ValidatePostalCode(string input)
        {
            if (string.IsNullOrEmpty(input))
                return true;

            return this.postalCodeFormat.IsMatch(input);
        }

        private bool ValidateVatNumber(string input)
        {
            if (string.IsNullOrEmpty(input))
                return true;

            return this.nifFormat.IsMatch(input);
        }

        private bool ValidateVatType(int input)
        {
            return input >= 0 && input <= 8;
        }
    }
}
