namespace a3innuva.TAA.Migration.SDK.Implementations
{
    using a3innuva.TAA.Migration.SDK.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;


    public class FixedAssetValidation : Validation<IFixedAsset>
    {
        private readonly IValidation<IFixedAssetDepreciationQuota> depreciationQuotaValidation;
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

            this.CreateRule(x => this.Validate(x.AccountCode), this.ReplaceInMessage(ValidationMessages.Mandatory, "'Cuenta de inmovilizado'"));
            this.CreateRule(x => this.Validate(x.AccountCode, 20), this.ReplaceInMessage(ValidationMessages.InvalidLength, "'Cuenta de inmovilizado'"));
            this.CreateRule(x => this.accountCodeFormat.IsMatch(x.AccountCode), this.ReplaceInMessage(ValidationMessages.InvalidFormat, "'Cuenta de inmovilizado'"));
            this.CreateRule(x => this.ValidateNullable(x.AccountDescription, 255), this.ReplaceInMessage(ValidationMessages.InvalidLength, "'Descripcion cuenta de inmovilizado'"));

            this.CreateRule(x => this.Validate(x.TypeOfGood), this.ReplaceInMessage(ValidationMessages.Mandatory, "'Tipo de bien'"));
            this.CreateRule(x => this.ValidateTypeOfGood(x.TypeOfGood), this.ReplaceInMessage(ValidationMessages.InvalidFormat, "'Tipo de bien'"));

            this.CreateRule(x => this.Validate(x.Description), this.ReplaceInMessage(ValidationMessages.Mandatory, "'Literal'"));

            this.CreateRule(x => this.ValidateNullable(x.AccumulatedDepreciationAccountCode, 20), this.ReplaceInMessage(ValidationMessages.InvalidLength, "'Cuenta de amortización acumulada'"));
            this.CreateRule(x => this.ValidateAccountFormat(x.AccumulatedDepreciationAccountCode), this.ReplaceInMessage(ValidationMessages.InvalidFormat, "'Cuenta de amortización acumulada'"));
            this.CreateRule(x => this.ValidateNullable(x.AccumulatedDepreciationAccountDescription, 255), this.ReplaceInMessage(ValidationMessages.InvalidLength, "'Descripcion cuenta de amortización acumulada'"));

            this.CreateRule(x => this.ValidateNullable(x.EndowmentAccountCode, 20), this.ReplaceInMessage(ValidationMessages.InvalidLength, "'Cuenta de dotación'"));
            this.CreateRule(x => this.ValidateAccountFormat(x.EndowmentAccountCode), this.ReplaceInMessage(ValidationMessages.InvalidFormat, "'Cuenta de dotación'"));
            this.CreateRule(x => this.ValidateNullable(x.EndowmentAccountDescription, 255), this.ReplaceInMessage(ValidationMessages.InvalidLength, "'Descripcion cuenta de amortización acumulada'"));

            this.CreateRule(x => this.ValidateNullable(x.AcquisitionVatNumber, 20), this.ReplaceInMessage(ValidationMessages.InvalidLength, "'NIF'"));
            this.CreateRule(x => this.ValidateVatNumber(x.AcquisitionVatNumber), this.ReplaceInMessage(ValidationMessages.InvalidFormat, "'NIF'"));

            this.CreateRule(x => this.ValidateNullable(x.AcquisitionPartnerName, 255), this.ReplaceInMessage(ValidationMessages.InvalidLength, "'Nombre de cliente'"));

            this.CreateRule(x => this.ValidateNullable(x.AcquisitionPartnerAccount, 20), this.ReplaceInMessage(ValidationMessages.InvalidLength, "'Cuenta de proveedor'"));
            this.CreateRule(x => this.ValidateAccountFormat(x.AcquisitionPartnerAccount), this.ReplaceInMessage(ValidationMessages.InvalidFormat, "'Cuenta de proveedor'"));

            this.CreateRule(x => this.ValidateVatType(x.AcquisitionVatType), this.ReplaceInMessage(ValidationMessages.InvalidFormat, "'Tipo de documento'"));
            this.CreateRule(x => this.ValidatePostalCode(x.AcquisitionPostalCode), this.ReplaceInMessage(ValidationMessages.InvalidFormat, "'Código postal'"));
            this.CreateRule(x => this.ValidateNullable(x.AcquisitionCountryCode, 2), this.ReplaceInMessage(ValidationMessages.InvalidLength, "'Código país'"));

            this.CreateRule(x => this.Validate(x.AcquisitionDate), this.ReplaceInMessage(ValidationMessages.Mandatory, "'Fecha de adquisición'"));

            this.CreateRule(x => this.Validate(x.AcquisitionValue), this.ReplaceInMessage(ValidationMessages.Mandatory, "'Valor de adquisición'"));

            this.CreateRule(x => this.ValidateNullable(x.AcquisitionInvoiceNumber, 60), this.ReplaceInMessage(ValidationMessages.InvalidLength, "'Número de factura'"));

            this.CreateRule(x => this.ValidateNullablePercentage(x.AcquisitionProrateAmount), this.ReplaceInMessage(ValidationMessages.InvalidFormat, "'Prorrata aplicada'"));

            this.CreateRule(x => this.Validate(x.AssetEndDate), this.ReplaceInMessage(ValidationMessages.InvalidFormat, "'Fecha de operación'"));

            this.CreateRule(x => this.ValidateRetirementReason(x.RetirementReason), this.ReplaceInMessage(ValidationMessages.InvalidFormat, "'Motivo de baja'"));

            this.CreateRule(x => this.ValidateSaleField(x.RetirementReason, x.RetirementInvoiceNumber), this.ReplaceInMessage(ValidationMessages.InvalidValue, "'Numero de factura de datos de baja'"));
            this.CreateRule(x => this.ValidateSaleField(x.RetirementReason, x.RetirementDisposalValue), this.ReplaceInMessage(ValidationMessages.InvalidValue, "'Valor de enajenación'"));
            this.CreateRule(x => this.ValidateSaleField(x.RetirementReason, x.RetirementBaseAmount), this.ReplaceInMessage(ValidationMessages.InvalidValue, "'Base imponinble de datos de baja'"));
            this.CreateRule(x => this.ValidateSaleField(x.RetirementReason, x.RetirementTaxCode), this.ReplaceInMessage(ValidationMessages.InvalidValue, "'Tipo de iva de datos de baja'"));
            this.CreateRule(x => this.ValidateSaleField(x.RetirementReason, x.RetirementTaxAmount), this.ReplaceInMessage(ValidationMessages.InvalidValue, "'Cuota de datos de baja'"));
            this.CreateRule(x => this.ValidateSaleField(x.RetirementReason, x.RetirementIsExempt), this.ReplaceInMessage(ValidationMessages.InvalidValue, "'Entrega exenta o no sujeta'"));

            this.CreateRule(x => this.Validate(x.DepreciationYears), this.ReplaceInMessage(ValidationMessages.Mandatory, "'Vida útil amortización contable'"));
            this.CreateRule(x => this.ValidatNotNegative(x.DepreciationYears), this.ReplaceInMessage(ValidationMessages.InvalidFormat, "'Vida útil amortización contable'"));
            
            this.CreateRule(x => this.Validate(x.DepreciationFiscalYears), this.ReplaceInMessage(ValidationMessages.Mandatory, "'Vida útil amortización fiscal'"));
            this.CreateRule(x => this.ValidatNotNegative(x.DepreciationFiscalYears), this.ReplaceInMessage(ValidationMessages.InvalidFormat, "'Vida útil amortización fiscal'"));

            this.CreateRule(x => this.Validate(x.AssetStartDate), this.ReplaceInMessage(ValidationMessages.Mandatory, "'Fecha de inicio de amortización'"));

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

        private bool ValidateDepreciationQuotas(IEnumerable<IFixedAssetDepreciationQuota> depreciationQuotas)
        {
            return depreciationQuotas != null && depreciationQuotas.Any();
        }

        private bool ValidateAccountFormat(string input)
        {
            if (string.IsNullOrEmpty(input))
                return true;

            return this.accountCodeFormat.IsMatch(input);
        }

        private bool ValidateTypeOfGood(int input)
        {
            return input >= 0 && input <= 4;
        }

        private bool ValidateRetirementReason(int? input)
        {
            return input == null || (input >= 1 && input <= 7);
        }

        private bool ValidatNotNegative(decimal? input)
        {
            return input > 0;
        }

        private bool ValidateSaleField(int? retirementReason, decimal? input)
        {
            return (input != null && retirementReason == 2) || input == null;
        }

        private bool ValidateSaleField(int? retirementReason, string input)
        {
            return (!string.IsNullOrEmpty(input) && retirementReason == 2) || string.IsNullOrEmpty(input);
        }

        private bool ValidateSaleField(int? retirementReason, bool input)
        {
            return (input == true && retirementReason == 2) || input == false;
        }
    }
}
