using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("a3innuva.TAA.Migration.SDK.Implementations")]
namespace a3innuva.TAA.Migration.SDK.Interfaces
{

    internal static class PartnerExtensions
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
    }
}
