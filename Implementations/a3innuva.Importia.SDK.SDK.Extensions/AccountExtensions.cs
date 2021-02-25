namespace a3innuva.TAA.Migration.SDK.Extensions
{
    using a3innuva.TAA.Migration.SDK.Interfaces;

    public static class AccountExtensions
    {
        public static bool HasVatNumber(this IAccount source)
        {
            return !string.IsNullOrEmpty(source.VatNumber);
        }

        public static bool HasPostalCode(this IAccount source)
        {
            return !string.IsNullOrEmpty(source.PostalCode);
        }

        public static bool HasName(this IAccount source)
        {
            return !string.IsNullOrEmpty(source.Name);
        }

        public static bool IsAPerson(this IAccount source)
        {
            return source.HasVatNumber() && source.HasName();
        }

        public static bool IsAPartner(this IAccount source)
        {
            return source.HasName();
        }
    }
}
