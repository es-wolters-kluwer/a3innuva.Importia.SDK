namespace a3innuva.TAA.Migration.SDK.Implementations
{
    using a3innuva.TAA.Migration.SDK.Interfaces;
    using System;

    public class InputInvoiceAdditionalData : IInputInvoiceAdditionalData
    {
        public string Description { get; set; }
        public DateTime RegistryDate { get; set; }
        public string DuaDocumentId { get; set; }
        public bool InvoicesInSpecialRegime { get; set; }
        public decimal TaxableIncomeAtCost { get; set; }
        public string InitialNumberOfDocument { get; set; }
        public string LastNumberOfDocument { get; set; }
        public TypeOfDocumentAdditionalDataEnum TypeOfDocument { get; set; }
        public FundamentalAdditionalDataEnum Fundamental { get; set; }
        public Guid Id { get; set; }
        public int Line { get; set; }
        public string Source { get; set; }

        public string Identity() => string.Empty;
    }
}
