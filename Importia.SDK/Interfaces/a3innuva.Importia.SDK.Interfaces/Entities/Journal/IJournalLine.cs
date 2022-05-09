namespace a3innuva.TAA.Migration.SDK.Interfaces
{
    /// <summary>
    /// Journal line
    /// </summary>
    public interface IJournalLine : IMigrationEntity
    {
        /// <summary>
        /// Line document, optional [0-60]
        /// </summary>
        string Document { get; set; }
        /// <summary>
        /// Line Concept, optional [0-255]
        /// </summary>
        string Concept { get; set; }
        /// <summary>
        /// Account code, mandatory [6-20]
        /// </summary>
        string Account { get; set; }
        /// <summary>
        /// Account description, mandatory [1-255]
        /// </summary>
        string AccountDescription { get; set; }
        /// <summary>
        /// Debit amount on format X.XX
        /// </summary>
        decimal Debit { get; set; }
        /// <summary>
        /// Debit amount on format X.XX
        /// </summary>
        decimal Credit { get; set; }
    }
}
