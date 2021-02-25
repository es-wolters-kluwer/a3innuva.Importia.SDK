namespace a3innuva.TAA.Migration.SDK.Interfaces
{
    public interface IMigrationInfo
    {
        string VatNumber { get; set; }
        int Year { get; set; }
        MigrationType Type { get; set; }
        MigrationOrigin Origin { get; set; }
        string FileName { get; set; }
        string Version { get; set; }
    }
}
