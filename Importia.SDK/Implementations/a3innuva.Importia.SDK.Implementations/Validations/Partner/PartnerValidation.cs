namespace a3innuva.TAA.Migration.SDK.Implementations
{
    using System;
    using System.Text.RegularExpressions;
    using a3innuva.TAA.Migration.SDK.Interfaces;

    public abstract class PartnerValidation : Validation<IPartner>
    {
        private readonly Regex accountCodeFormat; 
        private readonly Regex nifFormat;
        private readonly Regex postalCodeFormat;
        private readonly Regex countryCodeFormat = new Regex(@"^[A-Z]{2}$", RegexOptions.Compiled, TimeSpan.FromSeconds(5));
        // NIF (incl. K, L, M), NIE and CIF
        private readonly Regex spanishDocumentFormat = new Regex(@"^([0-9]{8}[A-Z]|[KLM][0-9]{7}[A-Z]|[XYZ][0-9]{7}[A-Z]|[ABCDEFGHJNPQRSUVW][0-9]{7}[0-9A-J])$", RegexOptions.Compiled, TimeSpan.FromSeconds(5));
        private readonly Regex intraCommunityVatFormat = new Regex(@"^[A-Z]{2}[A-Z0-9]+$", RegexOptions.Compiled, TimeSpan.FromSeconds(5));

        protected PartnerValidation()
        {
            this.accountCodeFormat = new Regex(@"^[1-9]{1}[0-9]*$", RegexOptions.Compiled, TimeSpan.FromSeconds(5));
            this.nifFormat = new Regex(@"^[A-Z0-9]*$", RegexOptions.Compiled, TimeSpan.FromSeconds(5));
            this.postalCodeFormat = new Regex(@"^[0-9]{5}$", RegexOptions.Compiled, TimeSpan.FromSeconds(5));
        }

        protected override void SetupValidations()
        {
            this.CreateRule(x => this.Validate(x.Id), "Id");
            this.CreateRule(x => this.Validate(x.Name), this.ReplaceInMessage(ValidationMessages.Mandatory, "'Compañia'"));
            this.CreateRule(x => this.Validate(x.Taxation),
                this.ReplaceInMessage(ValidationMessages.InvalidFormat, "'Tributación'"));
            this.CreateRule(x => this.ValidateNullable(x.VatNumber, 20), this.ReplaceInMessage(ValidationMessages.InvalidLength, "'NIF'"));
            this.CreateRule(x => this.ValidateVatNumber(x.VatNumber), this.ReplaceInMessage(ValidationMessages.InvalidFormat, "'NIF'"));
            this.CreateRule(x => this.ValidatePostalCode(x.PostalCode), this.ReplaceInMessage(ValidationMessages.InvalidFormat, "'Código postal'"));
            this.CreateRule(x => this.ValidateCountryCode(x.CountryCode), this.ReplaceInMessage(ValidationMessages.InvalidFormat, "'País'"));
            this.CreateRule(x => this.ValidateCountryForSpanishDocument(x.VatNumber, x.CountryCode), this.ReplaceInMessage("Para un NIF, CIF o NIE el único país válido es 'ES'"));
            this.CreateRule(x => this.ValidateCountryForIntraCommunityVat(x.VatNumber, x.CountryCode), this.ReplaceInMessage("Los dos primeros caracteres del NIF intracomunitario deben coincidir con el país"));
            this.CreateRule(x => this.ValidateNullable(x.CounterPartAccountCode, 20), this.ReplaceInMessage(ValidationMessages.InvalidLength, "'Contrapartida'"));
            this.CreateRule(x => this.ValidateAccountFormat(x.CounterPartAccountCode), this.ReplaceInMessage(ValidationMessages.InvalidFormat, "'Contrapartida'"));
            this.CreateRule(x => x.MaturitiesAccountCode == null || x.MaturitiesAccountCode.Length <= 20, this.ReplaceInMessage(ValidationMessages.InvalidLength, "'Cuenta bancaria'"));
            this.CreateRule(x => x.MaturitiesAccountCode == null || this.accountCodeFormat.IsMatch(x.MaturitiesAccountCode), this.ReplaceInMessage(ValidationMessages.InvalidFormat, "'Cuenta bancaria'"));
            this.CreateRule(x => this.Validate(x.TransactionCode), this.ReplaceInMessage(ValidationMessages.Mandatory, "'Operación'"));
            this.CreateRule(x => this.ValidateTransaction(x.TransactionCode), this.ReplaceInMessage("No es una operación valida"));
            this.CreateRule(x => this.ValidateWithHolding(x.WithHoldingCode), this.ReplaceInMessage("No es una retención valida"));
            //this.CreateRule(x => this.Validate(x.MaturitiesPeriodicity), this.ReplaceInMessage(ValidationMessages.Mandatory, "'Periodificación'"));
            this.CreateRule(x => this.Validate(x.MaturitiesPeriodicity) && this.ValidatePeriodicity(x.MaturitiesPeriodicity), this.ReplaceInMessage(ValidationMessages.InvalidFormat, "'Periodificación'"));
            this.CreateRule(x => this.ValidateVatType(x.VatType), this.ReplaceInMessage(ValidationMessages.InvalidFormat, "'Tipo de documento'"));
		}

        public abstract bool ValidateTransaction(string input);

        public abstract bool ValidateWithHolding(string input);

        private bool ValidateAccountFormat(string input)
        {
            if (string.IsNullOrEmpty(input))
                return true;

            return this.accountCodeFormat.IsMatch(input);
        }

        private bool ValidatePostalCode(string input)
        {
            if (string.IsNullOrEmpty(input))
                return true;

            return this.postalCodeFormat.IsMatch(input);
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

        private bool ValidateVatNumber(string input)
        {
            if (string.IsNullOrEmpty(input))
                return true;

            return this.nifFormat.IsMatch(input);
        }

        private bool Validate(Taxation taxation)
        {
            return taxation == Taxation.State || taxation == Taxation.CanaryIsland;
        }

        private bool Validate(int[] periodicity)
        {
            return periodicity != null;
        }

        private bool ValidatePeriodicity(int[] periodicity)
        {
            if (periodicity is null) return true;

            var allBiggerOrEqualThanZero = true;
            var anyBiggerThanZero = false;
            foreach (int num in periodicity)
            {
                if (num < 0)
                {
                    allBiggerOrEqualThanZero = false;
                    break;
                }
                if (num > 0)
                {
                    anyBiggerThanZero = true;
                }
            }

            return allBiggerOrEqualThanZero && anyBiggerThanZero;
        }

        private bool ValidateVatType(int vatType)
        {
            return vatType >= 0 && vatType <= 8;
        }
    }
}
