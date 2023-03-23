namespace a3innuva.TAA.Migration.SDK.Interfaces
{
    /// <summary>
    /// Migration manifest info
    /// </summary>
    public interface IMigrationInfo
    {
        /// <summary>
        /// Company vat number, mandatory
        /// </summary>
        string VatNumber { get; set; }
        /// <summary>
        /// Fiscal year, mandatory for journals and invoices
        /// </summary>
        int Year { get; set; }
        /// <summary>
        /// Entity's type
        /// </summary>
        MigrationType Type { get; set; }
        /// <summary>
        /// Source of migration
        /// </summary>
        MigrationOrigin Origin { get; set; }
        /// <summary>
        /// The file name, optional
        /// </summary>
        string FileName { get; set; }
        /// <summary>
        /// Version of set, it's must be match with the valid version on a3innuva.Importia suite, Actual 2.0
        /// </summary>
        string Version { get; set; }
        /// <summary>
        /// Activity Id
        /// </summary>
        string CorrelationActivityId { get; set; }
        /// <summary>
        /// Channel Id
        /// </summary>
        string CorrelationChannelId { get; set; }

        bool IsValidOrigin();
        bool IsValidType();
        bool IsValidYear();
        bool IsValidVatNumber();
        bool IsValidVersion();
        bool IsValid();
    }
}
