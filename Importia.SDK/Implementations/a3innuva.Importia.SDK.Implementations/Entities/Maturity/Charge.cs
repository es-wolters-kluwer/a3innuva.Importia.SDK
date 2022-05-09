namespace a3innuva.TAA.Migration.SDK.Implementations
{
    using a3innuva.TAA.Migration.SDK.Interfaces;
    using System;

    public class Charge : ICharge
    {
        public Guid Id { get; set; }
        public int Line { get; set; }
        public DateTime Date { get; set; }
        public ChargeSituation Situation { get; set; }
        public string BankAccount { get; set; }
        public string BankAccountDescription { get; set; }
        public decimal Amount { get; set; }
        public bool AccountingAffect { get; set; }

        public string Identity() => string.Empty;
    }
}
