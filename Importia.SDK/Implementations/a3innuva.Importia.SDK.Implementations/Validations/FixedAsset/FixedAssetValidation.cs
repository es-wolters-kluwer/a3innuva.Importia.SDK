namespace a3innuva.TAA.Migration.SDK.Implementations
{
    using a3innuva.TAA.Migration.SDK.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;


    public class FixedAssetValidation : Validation<IFixedAsset>
    {
        private readonly IValidation<IDepreciationQuota> depreciationQuotaValidation;
        private readonly Regex accountCodeFormat;
        private readonly Regex nifFormat;
        private readonly Regex postalCodeFormat;

        public FixedAssetValidation()
        {
            this.depreciationQuotaValidation = new DepreciationQuotaValidation();
            this.accountCodeFormat = new Regex(@"^[1-9]{1}[0-9]*$", RegexOptions.Compiled, TimeSpan.FromSeconds(5));
            this.nifFormat = new Regex(@"^[A-Z0-9]*$", RegexOptions.Compiled, TimeSpan.FromSeconds(5));
            this.postalCodeFormat = new Regex(@"^[0-9]{5}$", RegexOptions.Compiled, TimeSpan.FromSeconds(5));
        }

        protected override void SetupValidations()
        {
            this.CreateRule(x => this.Validate(x.Id), "Id");

            this.CreateRule(x => this.Validate(x.IdentificationAccountCode), this.ReplaceInMessage(ValidationMessages.Mandatory, "'Cuenta de inmovilizado'"));
            this.CreateRule(x => this.Validate(x.IdentificationAccountCode, 20), this.ReplaceInMessage(ValidationMessages.InvalidLength, "'Cuenta de inmovilizado'"));
            this.CreateRule(x => this.accountCodeFormat.IsMatch(x.IdentificationAccountCode), this.ReplaceInMessage(ValidationMessages.InvalidFormat, "'Cuenta de inmovilizado'"));

            this.CreateRule(x => this.Validate(x.IdentificationTypeOfGood), this.ReplaceInMessage(ValidationMessages.Mandatory, "'Tipo de bien'"));

            this.CreateRule(x => this.Validate(x.IdentificationDescription), this.ReplaceInMessage(ValidationMessages.Mandatory, "'Literal'"));

            this.CreateRule(x => this.ValidateNullable(x.IdentificationAccumulatedDepreciationAccountCode, 20), this.ReplaceInMessage(ValidationMessages.InvalidLength, "'Cuenta de amortización acumulada'"));
            this.CreateRule(x => this.ValidateAccountFormat(x.IdentificationAccumulatedDepreciationAccountCode), this.ReplaceInMessage(ValidationMessages.InvalidFormat, "'Cuenta de amortización acumulada'"));

            this.CreateRule(x => this.ValidateNullable(x.IdentificationEndowmentAccountCode, 20), this.ReplaceInMessage(ValidationMessages.InvalidLength, "'Cuenta de dotación'"));
            this.CreateRule(x => this.ValidateAccountFormat(x.IdentificationAccumulatedDepreciationAccountCode), this.ReplaceInMessage(ValidationMessages.InvalidFormat, "'Cuenta de dotación'"));

            this.CreateRule(x => this.ValidateNullable(x.IdentificationVatNumber, 20), this.ReplaceInMessage(ValidationMessages.InvalidLength, "'NIF'"));
            this.CreateRule(x => this.ValidateVatNumber(x.IdentificationVatNumber), this.ReplaceInMessage(ValidationMessages.InvalidFormat, "'NIF'"));

            this.CreateRule(x => this.ValidateNullable(x.IdentificationPartnerName, 255), this.ReplaceInMessage(ValidationMessages.InvalidLength, "'Nombre de cliente'"));

            this.CreateRule(x => this.ValidateNullable(x.IdentificationPartnerAccount, 20), this.ReplaceInMessage(ValidationMessages.InvalidLength, "'Cuenta de proveedor'"));
            this.CreateRule(x => this.ValidateAccountFormat(x.IdentificationPartnerAccount), this.ReplaceInMessage(ValidationMessages.InvalidFormat, "'Cuenta de proveedor'"));

            this.CreateRule(x => this.ValidateVatType(x.IdentificationVatType), this.ReplaceInMessage(ValidationMessages.InvalidFormat, "'Tipo de documento'"));
            this.CreateRule(x => this.ValidatePostalCode(x.IdentificationPostalCode), this.ReplaceInMessage(ValidationMessages.InvalidFormat, "'Código postal'"));
            this.CreateRule(x => this.ValidateNullable(x.IdentificationCountryCode, 2), this.ReplaceInMessage(ValidationMessages.InvalidLength, "'Código país'"));

            this.CreateRule(x => this.Validate(x.IdentificationAcquisitionDate), this.ReplaceInMessage(ValidationMessages.Mandatory, "'Fecha de adquisición'"));

            this.CreateRule(x => this.Validate(x.IdentificationAcquisitionValue), this.ReplaceInMessage(ValidationMessages.Mandatory, "'Valor de adquisición'"));

            this.CreateRule(x => this.Validate(x.IdentificationInvoiceNumber, 60), this.ReplaceInMessage(ValidationMessages.InvalidLength, "'Número de factura'"));

            this.CreateRule(x => this.ValidateNullablePercentage(x.IdentificationProrateAmountValue), this.ReplaceInMessage(ValidationMessages.InvalidFormat, "'Prorrata aplicada'"));

            this.CreateRule(x => this.Validate(x.IdentificationAssetEndDate), this.ReplaceInMessage(ValidationMessages.InvalidFormat, "'Fecha de operación'"));

            this.CreateRule(x => this.Validate(x.RepaymentDataPercentageDepreciation), this.ReplaceInMessage(ValidationMessages.Mandatory, "'% amortización contable'"));
            this.CreateRule(x => this.ValidateNullablePercentage(x.RepaymentDataPercentageDepreciation), this.ReplaceInMessage(ValidationMessages.InvalidFormat, "'% amortización contable'"));

            this.CreateRule(x => this.Validate(x.RepaymentDataPercentageFiscalDepreciation), this.ReplaceInMessage(ValidationMessages.Mandatory, "'% amortización fiscal'"));
            this.CreateRule(x => this.ValidateNullablePercentage(x.RepaymentDataPercentageFiscalDepreciation), this.ReplaceInMessage(ValidationMessages.InvalidFormat, "'% amortización fiscal'"));

            this.CreateRule(x => this.Validate(x.RepaymentDataAssetStartDate), this.ReplaceInMessage(ValidationMessages.Mandatory, "'Fecha de inicio de amortización'"));

            this.CreateRule(x => this.ValidateDepreciationQuotas(x.DepreciationQuotas), this.ReplaceInMessage(ValidationMessages.Mandatory, "'Plan de amortización'"));
        }

        public override IEnumerable<IValidationResult> Validate(IFixedAsset entity)
        {
            var errors = new List<IValidationResult>();

            foreach (var item in entity.DepreciationQuotas)
            {
                var result = depreciationQuotaValidation.Validate(item);

                var resultErrors = result.Where(x => !x.IsValid);
                errors.AddRange(resultErrors);
            }

            var entityErrors = base.Validate(entity);

            errors.AddRange(entityErrors);
            return errors;
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

        private bool ValidateNullablePercentage(decimal? input)
        {
            return input == null || (input >= 0 && input <= 100);
        }

        private bool ValidateDepreciationQuotas(IEnumerable<IDepreciationQuota> depreciationQuotas)
        {
            return depreciationQuotas != null && depreciationQuotas.Any();
        }

        private bool ValidateAccountFormat(string input)
        {
            if (string.IsNullOrEmpty(input))
                return true;

            return this.accountCodeFormat.IsMatch(input);
        }
    }
}
