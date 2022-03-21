namespace a3innuva.TAA.Migration.SDK.Interfaces
{
    using System.Collections.Generic;

    public class TypeOfDocumentAdditionalData
    {
        private static readonly List<string> outputTypeOfDocuments;
        private static readonly List<string> inputTypeOfDocuments;

        static TypeOfDocumentAdditionalData()
        {
            outputTypeOfDocuments = new List<string>()
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

            inputTypeOfDocuments = new List<string>()
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

        public static bool ItExistForOutput(string code)
        {
            return outputTypeOfDocuments.Contains(code);
        }

        public static bool ItExistForInput(string code)
        {
            return inputTypeOfDocuments.Contains(code);
        }
    }
}
