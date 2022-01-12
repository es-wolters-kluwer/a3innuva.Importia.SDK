namespace a3innuva.TAA.Migration.SDK.Interfaces
{
    using System.Collections.Generic;

    public class TypeOfDocumentAdditionalData
    {
        private readonly List<string> outputTypeOfDocuments;
        private readonly List<string> inputTypeOfDocuments;

        public TypeOfDocumentAdditionalData()
        {
            this.outputTypeOfDocuments = new List<string>()
            {
                "TIPO_FACTURA_COMPLETA",
                "TIPO_SIMPLIFICADA",
                "TIPO_FACTURA_SIN_NIF",
                "TIPO_RECT_ERROR_FUNDADO",
                "TIPO_RECT_CONCURSO",
                "TIPO_RECT_DEUDA_INCOBRABLE",
                "TIPO_RECT_RESTO",
                "TIPO_RECT_SIMPLIFICADA"
            };

            this.inputTypeOfDocuments = new List<string>()
            {
                "TIPO_FACTURA_COMPLETA",
                "TIPO_SIMPLIFICADA",
                "TIPO_JUSTIFICANTES_CONTABLES",
                "TIPO_RECT_ERROR_FUNDADO",
                "TIPO_RECT_CONCURSO",
                "TIPO_RECT_DEUDA_INCOBRABLE",
                "TIPO_RECT_RESTO",
                "TIPO_RECT_SIMPLIFICADA"
            };
        }

        public bool ItExistForOutput(string code)
        {
            return this.outputTypeOfDocuments.Contains(code);
        }

        public bool ItExistForInput(string code)
        {
            return this.inputTypeOfDocuments.Contains(code);
        }
    }
}
