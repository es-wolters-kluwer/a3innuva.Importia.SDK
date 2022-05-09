namespace a3innuva.TAA.Migration.SDK.Interfaces
{
    using System;

    public interface IMaturity : IMigrationEntity
    {
        DateTime Date { get; set; }
        string BankAccount { get; set; }
        string BankAccountDescription { get; set; }
        decimal Amount { get; set; }
        bool AccountingAffect { get; set; }
    }
}
