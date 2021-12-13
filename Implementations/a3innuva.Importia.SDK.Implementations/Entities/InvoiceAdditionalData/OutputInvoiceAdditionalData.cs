namespace a3innuva.TAA.Migration.SDK.Implementations 
{
    using a3innuva.TAA.Migration.SDK.Interfaces;
    using System;

    public class OutputInvoiceAdditionalData : IOutputInvoiceAdditionalData
    {
        public string Description { get; set; }
        public bool InvoiceByThird { get; set; }
        public bool MultipleRecipients { get; set; }
        public bool CouponsBonusesOrDiscounts { get; set; }
        public bool InvoicesInSpecialRegime { get; set; }
        public decimal TaxableIncomeAtCost { get; set; }
        public string InitialNumberOfDocument { get; set; }
        public string LastNumberOfDocument { get; set; }
        public bool NotIncludedInCensus { get; set; }
        public TypeOfDocumentAdditionalDataEnum TypeOfDocument { get; set; }
        public FundamentalAdditionalDataEnum Fundamental { get; set; }
        public Guid Id { get; set; }
        public int Line { get; set; }
        public string Source { get; set; }

        public string Identity() => string.Empty;
    }
}
