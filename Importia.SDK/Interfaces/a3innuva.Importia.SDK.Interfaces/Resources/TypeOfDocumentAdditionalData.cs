namespace a3innuva.TAA.Migration.SDK.Interfaces
{
    using System.Collections.Generic;

    public static class TypeOfDocumentAdditionalData
    {
        private static readonly List<string> OutputTypeOfDocuments= new List<string>()
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
        private static readonly List<string> InputTypeOfDocuments= new List<string>()
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



        public static bool ItExistForOutput(string code)
        {
            return OutputTypeOfDocuments.Contains(code);
        }

        public static bool ItExistForInput(string code)
        {
            return InputTypeOfDocuments.Contains(code);
        }
    }
}
