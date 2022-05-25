﻿namespace a3innuva.TAA.Migration.SDK.Implementations
{
    using a3innuva.TAA.Migration.SDK.Interfaces;
    public class InputInvoiceAdditionalDataValidation : Validation<IInputInvoiceAdditionalData>
    {
        protected override void SetupValidations()
        {
            this.CreateRule(x => this.Validate(x.Id), "Id");

            this.CreateRule(x => x.Description.Length <= 500, this.ReplaceInMessage(ValidationMessages.InvalidLength, "'Descripción'"));

            this.CreateRule(x => x.DuaDocumentId.Length <= 18, this.ReplaceInMessage(ValidationMessages.InvalidLength, "'Número de DUA'"));

            this.CreateRule(x => this.ValidateNullable(x.InitialNumberOfDocument, 60), this.ReplaceInMessage(ValidationMessages.InvalidLength, "'Número de documento inicial'"));
            this.CreateRule(x => this.ValidateNullable(x.LastNumberOfDocument, 60), this.ReplaceInMessage(ValidationMessages.InvalidLength, "'Número de documento final'"));

            this.CreateRule(x => this.ValidateTypeOfDocument(x.TypeOfDocument), this.ReplaceInMessage("No es un tipo de documento válido"));
            this.CreateRule(x => this.ValidateFundamental(x.Fundamental), this.ReplaceInMessage("No es un tipo de clave válida"));
        }

        private bool ValidateTypeOfDocument(string input)
        {
            if (string.IsNullOrEmpty(input))
                return true;

            return TypeOfDocumentAdditionalData.ItExistForInput(input);
        }

        private bool ValidateFundamental(string input)
        {
            if (string.IsNullOrEmpty(input))
                return true;

            return FundamentalAdditionalData.ItExistForInput(input);
        }
    }
}