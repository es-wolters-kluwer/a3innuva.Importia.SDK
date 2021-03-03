namespace a3innuva.TAA.Migration.SDK.Interfaces
{
    /// <summary>
    /// Migration generic set
    /// </summary>
    public interface IMigrationSet
    {
        IMigrationInfo Info { get; set; }
        IMigrationEntity[] Entities { get; set; }
    }
}
