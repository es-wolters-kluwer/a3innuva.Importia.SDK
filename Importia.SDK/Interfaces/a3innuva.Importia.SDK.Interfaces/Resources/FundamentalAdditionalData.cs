namespace a3innuva.TAA.Migration.SDK.Interfaces
{
    using System.Collections.Generic;

    public static class FundamentalAdditionalData
    {
        private static readonly List<string> OutputFundamentals= new List<string>()
        {
            "CLAVE_REGIMEN_GENERAL",
            "CLAVE_AGENCIAS_VIAJES",
            "CLAVE_COBROS_HONORARIOS",
            "CLAVE_CERTIFICADOS_ADM_PUBLICA",
            "CLAVE_TRACTO_SUCESIVO",
            "CLAVE_AGENCIAS_VIAJE_MEDIADOR",
            "CLAVE_RECT_REGIMEN_GENERAL",
            "CLAVE_RECT_AGENCIAS_VIAJES",
            "CLAVE_RECT_COBROS_HONORARIOS",
            "CLAVE_RECT_CERTIFICADOS_ADM_PUBLICA",
            "CLAVE_RECT_TRACTO_SUCESIVO",
            "CLAVE_RECT_AGENCIAS_VIAJE_MEDIADOR"
        };
        private static readonly List<string> InputFundamentals = new List<string>()
        {
            "CLAVE_REGIMEN_GENERAL",
            "CLAVE_AGENCIAS_VIAJES",
            "CLAVE_AGENCIAS_VIAJES_MEDIACION",
            "CLAVE_RECT_REGIMEN_GENERAL",
            "CLAVE_RECT_AGENCIAS_VIAJES",
            "CLAVE_RECT_AGENCIAS_VIAJES_MEDIACION",
            "CLAVE_RECT_AGENCIAS_VIAJES_SERVICIOS"
        };
        
        public static bool ItExistForOutput(string code)
        {
            return OutputFundamentals.Contains(code);
        }

        public static bool ItExistForInput(string code)
        {
            return InputFundamentals.Contains(code);
        }
    }
}
