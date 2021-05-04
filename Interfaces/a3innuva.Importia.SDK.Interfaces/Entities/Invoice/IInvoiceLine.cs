namespace a3innuva.TAA.Migration.SDK.Interfaces
{
    public interface IInvoiceLine : IMigrationEntity
    {
        /// <summary>
        /// Index of line
        /// </summary>
        int Index { get; set; }
        /// <summary>
        /// Base amount, format X.XX
        /// </summary>
        decimal BaseAmount { get; set; }
        /// <summary>
        /// Counterpart account, [6-20] 
        /// </summary>
        string CounterPart{ get; set; }
        /// <summary>
        /// Counterpart account, optional [1-255]
        /// </summary>
        string CounterPartDescription{ get; set; }
        /// <summary>
        /// Tax amount, optional format X.XX, null for IGIC exent operations, zero for type zero IGIC operations
        /// </summary>
        decimal? TaxAmount { get; set; }
        /// <summary>
        /// Tax surcharge amount, format X.XX
        /// </summary>
        decimal? TaxSurchargeAmount { get; set; }
        /// <summary>
        /// Tax deductible amount, format X.XX
        /// </summary>
        decimal? TaxDeductibleAmount { get; set; }
        /// <summary>
        /// Tax not deductible amount, format X.XX
        /// </summary>
        decimal? TaxNonDeductibleAmount { get; set; }
        /// <summary>
        /// Tax deductible surcharge amount, format X.XX
        /// </summary>
        decimal? TaxDeductibleSurchargeAmount { get; set; }
        /// <summary>
        /// Tax not deductible surcharme amount, format X.XX
        /// </summary>
        decimal? TaxNonDeductibleSurchargeAmount { get; set; }
        /// <summary>
        /// WithHolding percentage, mandatory [0,1] format 0.XX or 1
        /// </summary>
        decimal? WithHoldingPercentage { get; set; }
        /// <summary>
        /// WithHolding amount, format X.XX
        /// </summary>
        decimal? WithHoldingAmount { get; set; }
        /// <summary>
        /// Transaction code. Go to valid transactions codes
        /// </summary>
        string Transaction { get; set; }
        /// <summary>
        /// WithHolding code. Go to valid withHolding codes
        /// </summary>
        string WithHolding { get; set; }

    }
}
