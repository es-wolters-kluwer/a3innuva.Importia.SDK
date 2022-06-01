namespace a3innuva.TAA.Migration.SDK.Interfaces
{
    using System;

    public interface IInvoice : IMigrationEntity, IMigrationSourceInfo
    {
        /// <summary>
        /// Invoice Correlation id
        /// </summary>
        string CorrelationId { get; set; }

        /// <summary>
        /// Invoice's date
        /// </summary>
        DateTime InvoiceDate { get; set; }
        /// <summary>
        /// Journal's invoice date, optional
        /// </summary>
        DateTime? JournalDate { get; set; }
        /// <summary>
        /// Invoice number, mandatory [1-60]
        /// </summary>
        string InvoiceNumber { get; set; }
        /// <summary>
        /// Transaction's invoice date, optional
        /// </summary>
        DateTime? TransactionDate { get; set; }
        /// <summary>
        /// Third party fiscal name, must be informed if vatNumber or postalCode was informed [0-255]
        /// </summary>
        string PartnerName { get; set; }
        /// <summary>
        /// Third party fiscal document, must be informed if name or postalCode was informed [0-20]
        /// </summary>
        string VatNumber { get; set; }
        /// <summary>
        /// Third party account code, mandatory [6-20]
        /// </summary>
        string PartnerAccount{ get; set; }
        /// <summary>
        /// For corrective invoices
        /// </summary>
        bool IsCorrective { get; set; }
        /// <summary>
        /// Corrective invoice number, for corrective invoices is mandatory[1-60]
        /// </summary>
        string CorrectiveInvoiceNumber { get; set; }
        /// <summary>
        /// Third party postal code, mandatory if name and postalCode wa informed [0-5]
        /// </summary>
        string PostalCode { get; set; }
        /// <summary>
        /// Country code on format ISO 3166-1 alpha-2, optional
        /// </summary>
        string CountryCode { get; set; }
        /// <summary>
        /// Third party vatNumber type, optional [0,8]
        /// </summary>
        int VatType { get; set; }
        /// <summary>
        /// To create and link a journal
        /// </summary>
        bool AccountingAffected { get; set; }
        /// <summary>
        /// Maturity pending amount, optional, format X.XX
        /// </summary>
        decimal ? PendingAmount { get; set; }
        /// <summary>
        /// Maturity satisfied amount, optional, format X.XX
        /// </summary>
        decimal ? SatisfiedAmount { get; set; }
    }
}
