namespace a3innuva.TAA.Migration.SDK.Interfaces
{
    using System.Collections.Generic;

    public static class TypeOfTaxOutputInvoice
	{
        private static readonly List<string> OutputTypeTax = new List<string>()
        {
            "EXENTO",
            "DONACIONES",
            "DONACIONES RECARGO"
        };

        public static bool ItExist(string code)
        {
            return OutputTypeTax.Contains(code);
        }
    }
}
