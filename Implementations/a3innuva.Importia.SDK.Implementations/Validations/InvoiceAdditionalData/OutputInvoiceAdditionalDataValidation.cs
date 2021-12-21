namespace a3innuva.TAA.Migration.SDK.Implementations
{
    using a3innuva.TAA.Migration.SDK.Interfaces;
    public class OutputInvoiceAdditionalDataValidation : Validation<IOutputInvoiceAdditionalData>
    {
        protected override void SetupValidations()
        {
            this.CreateRule(x => this.Validate(x.Id), "Id");

            this.CreateRule(x => x.Description.Length <= 500, this.ReplaceInMessage(ValidationMessages.InvalidLength, "'Descripción'"));

            this.CreateRule(x => this.ValidateNullable(x.InitialNumberOfDocument, 60), this.ReplaceInMessage(ValidationMessages.InvalidLength, "'Número de documento inicial'"));
            this.CreateRule(x => this.ValidateNullable(x.LastNumberOfDocument, 60), this.ReplaceInMessage(ValidationMessages.InvalidLength, "'Número de documento final'"));
        }
    }
}
