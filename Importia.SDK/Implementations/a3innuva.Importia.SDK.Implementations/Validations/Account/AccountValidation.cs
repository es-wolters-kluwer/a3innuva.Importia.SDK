namespace a3innuva.TAA.Migration.SDK.Implementations
{
    using System;
    using System.Text.RegularExpressions;
    using a3innuva.TAA.Migration.SDK.Interfaces;


    public class AccountValidation : Validation<IAccount>
    {
        private readonly Regex accountCodeFormat;
        private readonly Regex nifFormat;
        private readonly Regex postalCodeFormat;
        private readonly Regex countryCodeFormat = new Regex(@"^[A-Z]{2}$", RegexOptions.Compiled, TimeSpan.FromSeconds(5));
        // NIF (incl. K, L, M), NIE and CIF
        private readonly Regex spanishDocumentFormat = new Regex(@"^([0-9]{8}[A-Z]|[KLM][0-9]{7}[A-Z]|[XYZ][0-9]{7}[A-Z]|[ABCDEFGHJNPQRSUVW][0-9]{7}[0-9A-J])$", RegexOptions.Compiled, TimeSpan.FromSeconds(5));
        private readonly Regex intraCommunityVatFormat = new Regex(@"^[A-Z]{2}[A-Z0-9]+$", RegexOptions.Compiled, TimeSpan.FromSeconds(5));

        public AccountValidation()
        {
            this.accountCodeFormat = new Regex(@"^[1-9]{1}[0-9]*$", RegexOptions.Compiled, TimeSpan.FromSeconds(5));
            this.nifFormat = new Regex(@"^[A-Z0-9]*$", RegexOptions.Compiled, TimeSpan.FromSeconds(5));
            this.postalCodeFormat = new Regex(@"^[0-9]{5}$", RegexOptions.Compiled, TimeSpan.FromSeconds(5));
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
            this.CreateRule(x => this.ValidateCountryCode(x.CountryCode), this.ReplaceInMessage(ValidationMessages.InvalidFormat, "'Código país'"));
            this.CreateRule(x => this.ValidateCountryForSpanishDocument(x.VatNumber, x.CountryCode), this.ReplaceInMessage("Para un NIF, CIF o NIE el único país válido es 'ES'"));
            this.CreateRule(x => this.ValidateCountryForIntraCommunityVat(x.VatNumber, x.CountryCode), this.ReplaceInMessage("Los dos primeros caracteres del NIF intracomunitario deben coincidir con el país"));
        }

        private bool ValidateCountryCode(string input)
        {
            if (string.IsNullOrEmpty(input))
                return true;

            return this.countryCodeFormat.IsMatch(input);
        }

        private bool ValidateCountryForSpanishDocument(string vatNumber, string countryCode)
        {
            if (string.IsNullOrEmpty(vatNumber) || string.IsNullOrEmpty(countryCode))
                return true;

            return !this.spanishDocumentFormat.IsMatch(vatNumber) || countryCode == "ES";
        }

        private bool ValidateCountryForIntraCommunityVat(string vatNumber, string countryCode)
        {
            if (string.IsNullOrEmpty(vatNumber) || string.IsNullOrEmpty(countryCode))
                return true;

            if (this.spanishDocumentFormat.IsMatch(vatNumber) || !this.intraCommunityVatFormat.IsMatch(vatNumber))
                return true;

            return vatNumber.StartsWith(countryCode, StringComparison.Ordinal);
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
