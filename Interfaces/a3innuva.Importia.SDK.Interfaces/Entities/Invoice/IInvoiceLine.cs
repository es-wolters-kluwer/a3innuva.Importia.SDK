namespace a3innuva.TAA.Migration.SDK.Interfaces
{
    public interface IInvoiceLine : IMigrationEntity
    {
        int Index { get; set; }
        decimal BaseAmount { get; set; }
        decimal? TaxAmount { get; set; }
        decimal? TaxSurchargeAmount { get; set; }
        decimal? TaxDeductibleAmount { get; set; }
        decimal? TaxNonDeductibleAmount { get; set; }
        decimal? TaxDeductibleSurchargeAmount { get; set; }
        decimal? TaxNonDeductibleSurchargeAmount { get; set; }
        decimal? WithHoldingPercentage { get; set; }
        decimal? WithHoldingAmount { get; set; }
        string Transaction { get; set; }
        string WithHolding { get; set; }

    }
}
