using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("a3innuva.TAA.Migration.SDK.Implementations")]
namespace a3innuva.TAA.Migration.SDK.Interfaces
{

    internal static class AccountExtensions
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
    }
}
