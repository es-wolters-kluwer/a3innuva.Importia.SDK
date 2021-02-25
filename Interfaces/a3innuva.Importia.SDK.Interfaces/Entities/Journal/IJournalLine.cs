namespace a3innuva.TAA.Migration.SDK.Interfaces
{
    public interface IJournalLine : IMigrationEntity
    {
        string Document { get; set; }
        string Concept { get; set; }
        string Account { get; set; }
        string AccountDescription { get; set; }
        decimal Debit { get; set; }
        decimal Credit { get; set; }
    }
}
