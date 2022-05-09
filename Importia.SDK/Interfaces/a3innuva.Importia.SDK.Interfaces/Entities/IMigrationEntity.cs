namespace a3innuva.TAA.Migration.SDK.Interfaces
{
    using System;

    public interface IMigrationEntity
    {
        Guid Id { get; set; }
        int Line { get; set; }
        string Identity();
    }
}
