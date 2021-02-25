namespace a3innuva.TAA.Migration.SDK.Interfaces
{
    using System;

    public interface IJournal : IMigrationEntity, IMigrationSourceInfo
    {
        DateTime Date { get; set; }
        string Number { get; set; }
        string Document { get; set; }
        IJournalLine[] Lines { get; set; }
        JournalTypes Type { get; set; }
    }
}
