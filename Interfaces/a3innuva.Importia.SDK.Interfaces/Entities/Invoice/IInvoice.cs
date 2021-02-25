namespace a3innuva.TAA.Migration.SDK.Interfaces
{
    using System;

    public interface IInvoice : IMigrationEntity, IMigrationSourceInfo
    {
        DateTime InvoiceDate { get; set; }
        DateTime? JournalDate { get; set; }
        string InvoiceNumber { get; set; }
        DateTime? TransactionDate { get; set; }
        string PartnerName { get; set; }
        string VatNumber { get; set; }
        string PartnerAccount{ get; set; }
        bool IsCorrective { get; set; }
        string CorrectiveInvoiceNumber { get; set; }
        string PostalCode { get; set; }
        string CountryCode { get; set; }
        int VatType { get; set; }
        bool AccountingAffected { get; set; }
        decimal ? PendingAmount { get; set; }
        decimal ? SatisfiedAmount { get; set; }
    }
}
