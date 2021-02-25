using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("a3innuva.TAA.Migration.SDK.Implementations")]
namespace a3innuva.TAA.Migration.SDK.Interfaces
{
    internal static class JournalExtensions
    {
        public static bool HasAmount(this IJournalLine source)
        {
            return source.Debit != 0 || source.Credit != 0;
        }

        public static bool HasValidAmount(this IJournalLine source)
        {
            return (source.Debit != 0 && source.Credit == 0) || (source.Debit == 0 && source.Credit != 0);
        }

        public static bool IsAmountOnDebit(this IJournalLine source)
        {
            return source.Debit != 0;
        }

        public static bool IsAmountOnCredit(this IJournalLine source)
        {
            return source.Credit != 0;
        }

        public static decimal Amount(this IJournalLine source)
        {
            return source.IsAmountOnDebit() ? source.Debit : source.Credit;
        }
    }
}
