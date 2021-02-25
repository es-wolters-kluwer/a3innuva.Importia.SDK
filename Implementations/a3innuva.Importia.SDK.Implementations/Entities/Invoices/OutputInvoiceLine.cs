namespace a3innuva.TAA.Migration.SDK.Implementations
{
    using System;
    using a3innuva.TAA.Migration.SDK.Interfaces;

    public class OutputInvoiceLine : IOutputInvoiceLine
    {
        public Guid Id { get; set; }
        public int Line { get; set; }

        public string Identity()
        {
            return String.Empty;
        }

        public int Index { get; set; }
        public decimal BaseAmount { get; set; }
        public decimal? TaxAmount { get; set; }
        public decimal? TaxSurchargeAmount { get; set; }
        public decimal? TaxDeductibleAmount { get; set; }
        public decimal? TaxNonDeductibleAmount { get; set; }
        public decimal? TaxDeductibleSurchargeAmount { get; set; }
        public decimal? TaxNonDeductibleSurchargeAmount { get; set; }
        public decimal? WithHoldingPercentage { get; set; }
        public decimal? WithHoldingAmount { get; set; }
        public string Transaction { get; set; }
        public string WithHolding { get; set; }
    }
}
