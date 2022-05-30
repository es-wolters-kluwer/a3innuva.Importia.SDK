namespace a3innuva.TAA.Migration.SDK.Interfaces
{
    using System;
    /// <summary>
    /// Journal entity
    /// </summary>
    public interface IJournal : IMigrationEntity, IMigrationSourceInfo
    {
        /// <summary>
        /// Journal date
        /// </summary>
        DateTime Date { get; set; }
        /// <summary>
        /// Journal number
        /// </summary>
        string Number { get; set; }
        /// <summary>
        /// Header document
        /// </summary>
        string Document { get; set; }
        /// <summary>
        /// Lines
        /// </summary>
        IJournalLine[] Lines { get; set; }
        /// <summary>
        /// Journal type
        /// </summary>
        JournalTypes Type { get; set; }
        /// <summary>
        /// Invoice Correlation id
        /// </summary>
        string CorrelationInvoiceId { get; set; }
    }
}
