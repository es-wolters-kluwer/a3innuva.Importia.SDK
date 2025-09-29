namespace a3innuva.TAA.Migration.SDK.Extensions
{
    using a3innuva.TAA.Migration.SDK.Interfaces;

    public static class PartnerExtensions
    {
        public static bool HasVatNumber(this IPartner source)
        {
            return !string.IsNullOrEmpty(source.VatNumber);
        }

        public static bool HasPostalCode(this IPartner source)
        {
            return !string.IsNullOrEmpty(source.PostalCode);
        }

        public static bool HasName(this IPartner source)
        {
            return !string.IsNullOrEmpty(source.TradeName);
        }

        public static bool IsAPerson(this IPartner source)
        {
            return source.HasVatNumber() && source.HasName();
        }

        public static bool IsAPartner(this IPartner source)
        {
            return source.HasName();
        }
    }
}
