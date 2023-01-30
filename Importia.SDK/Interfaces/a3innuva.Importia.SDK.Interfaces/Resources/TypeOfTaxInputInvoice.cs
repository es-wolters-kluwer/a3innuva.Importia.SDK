namespace a3innuva.TAA.Migration.SDK.Interfaces
{
    using System.Collections.Generic;

    public static class TypeOfTaxInputInvoice
	{
        private static readonly List<string> TypeTax = new List<string>()
        {
            "EXENTO",
            "DONACIONES",
            "DONACIONES RECARGO"
        };

        public static bool ItExist(string code)
        {
            return TypeTax.Contains(code);
        }
    }
}
