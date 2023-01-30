namespace a3innuva.TAA.Migration.SDK.Interfaces
{
	using System;
	using System.Linq;

	public static class TypeOfTaxOutputInvoice
	{
        private static readonly string[] TypeTax = new string[]
        {
            "EXENTO",
            "DONACIONES",
            "DONACIONES RECARGO"
        };

        public static bool ItExist(string code)
        {
			return TypeTax.Contains(code, StringComparer.OrdinalIgnoreCase);
		}
    }
}
