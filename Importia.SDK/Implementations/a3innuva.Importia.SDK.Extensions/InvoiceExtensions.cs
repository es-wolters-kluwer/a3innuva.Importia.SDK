namespace a3innuva.TAA.Migration.SDK.Extensions
{
    using a3innuva.TAA.Migration.SDK.Interfaces;

    public static class InvoiceExtensions
    {
        
        public static bool HasVatNumber(this IInvoice source)
        {
            return !string.IsNullOrEmpty(source.VatNumber);
        }

        public static bool HasPostalCode(this IInvoice source)
        {
            return !string.IsNullOrEmpty(source.PostalCode);
        }
    }
}
