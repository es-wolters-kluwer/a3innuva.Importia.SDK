namespace a3innuva.TAA.Migration.SDK.Implementations
{
    using a3innuva.TAA.Migration.SDK.Interfaces;


    public class DepreciationQuotaValidation : Validation<IFixedAssetDepreciationQuota>
    {

        protected override void SetupValidations()
        {
            this.CreateRule(x => this.Validate(x.AccountingYear), this.ReplaceInMessage(ValidationMessages.Mandatory, "'Ejercicio contable'"));
            this.CreateRule(x => this.Validate(x.AccountingQuotaAmount), this.ReplaceInMessage(ValidationMessages.Mandatory, "'Cuota contable'"));
            this.CreateRule(x => this.Validate(x.FiscalQuotaAmount), this.ReplaceInMessage(ValidationMessages.Mandatory, "'Cuota fiscal'"));
            this.CreateRule(x => this.Validate(x.StartDate), this.ReplaceInMessage(ValidationMessages.Mandatory, "'Inicio de periodo de amortización'"));
            this.CreateRule(x => this.Validate(x.EndDate), this.ReplaceInMessage(ValidationMessages.Mandatory, "'Fin de periodo de amortización'"));
        }
    }
}
