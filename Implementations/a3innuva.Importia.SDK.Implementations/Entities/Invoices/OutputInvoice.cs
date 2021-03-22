namespace a3innuva.TAA.Migration.SDK.Implementations
{
    using System;
    using a3innuva.TAA.Migration.SDK.Interfaces;

    public class OutputInvoice : IOutputInvoice
    {
        public Guid Id { get; set; }
        public int Line { get; set; }
        public string Identity()
        {
            return $"{this.InvoiceDate.ToShortDateString()}-{this.JournalDate.GetValueOrDefault().ToShortDateString()}-{this.InvoiceNumber}-{this.PartnerAccount}";
        }
        public string Source { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime? JournalDate { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime? TransactionDate { get; set; }
        public IOutputInvoiceLine[] Lines { get; set; }
        public string PartnerName { get; set; }
        public string VatNumber { get; set; }
        public string PartnerAccount { get; set; }
        public bool IsCorrective { get; set; }
        public string CorrectiveInvoiceNumber { get; set; }
        public string PostalCode { get; set; }
        public string CountryCode { get; set; }
        public int VatType { get; set; }
        public bool AccountingAffected { get; set; }
        public decimal? PendingAmount { get; set; }
        public decimal? SatisfiedAmount { get; set; }
        public ICharge[] Maturities { get; set; }
    }
}
