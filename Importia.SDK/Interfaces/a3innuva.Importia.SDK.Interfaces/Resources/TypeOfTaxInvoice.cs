namespace a3innuva.TAA.Migration.SDK.Interfaces
{
    using System.Collections.Generic;

    public static class TypeOfTaxInvoice
	{
        private static readonly List<string> OutputTypeTax = new List<string>()
        {
            "EXENTO",
            "DONACIONES",
            "DONACIONES_RECARGO"
        };
        private static readonly List<string> InputTypeTax= new List<string>()
        {
			"EXENTO",
			"DONACIONES",
			"DONACIONES_RECARGO"
		};

        public static bool ItExistForOutput(string code)
        {
            return OutputTypeTax.Contains(code);
        }

        public static bool ItExistForInput(string code)
        {
            return InputTypeTax.Contains(code);
        }
    }
}
