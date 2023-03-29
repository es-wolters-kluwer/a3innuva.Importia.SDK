namespace a3innuva.TAA.Migration.SDK.Implementations
{
    using a3innuva.TAA.Migration.SDK.Interfaces;
    public class OutputInvoiceAdditionalDataValidation : Validation<IOutputInvoiceAdditionalData>
    {
        protected override void SetupValidations()
        {
            this.CreateRule(x => this.Validate(x.Id), "Id");

            this.CreateRule(x => this.ValidateDescription(x.Description), this.ReplaceInMessage(ValidationMessages.InvalidLength, "'Descripción adicional'"));

            this.CreateRule(x => this.ValidateNullable(x.InitialNumberOfDocument, 60), this.ReplaceInMessage(ValidationMessages.InvalidLength, "'Número de documento inicial'"));
            this.CreateRule(x => this.ValidateNullable(x.LastNumberOfDocument, 60), this.ReplaceInMessage(ValidationMessages.InvalidLength, "'Número de documento final'"));

            this.CreateRule(x => this.ValidateTypeOfDocument(x.TypeOfDocument), this.ReplaceInMessage("No es un tipo de documento válido"));
            this.CreateRule(x => this.ValidateFundamental(x.Fundamental), this.ReplaceInMessage("No es un tipo de clave válida"));
        }

        private bool ValidateDescription(string description) => string.IsNullOrEmpty(description) || description.Length <= 500;

        private bool ValidateTypeOfDocument(string input) => string.IsNullOrEmpty(input) || TypeOfDocumentAdditionalData.ItExistForOutput(input);

        private bool ValidateFundamental(string input) => string.IsNullOrEmpty(input) || FundamentalAdditionalData.ItExistForOutput(input);
    }
}
