namespace a3innuva.TAA.Migration.SDK.Implementations
{
    using System;
    using a3innuva.TAA.Migration.SDK.Interfaces;

    /// <summary>
    /// Account entity
    /// </summary>
    public class FixedAssetDepreciationQuota : IFixedAssetDepreciationQuota
    {
        public int AccountingYear { get; set; }
        public decimal AccountingQuotaAmount { get; set; }
        public decimal FiscalQuotaAmount { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Guid Id { get; set; }
        public int Line { get; set; }

        public string Identity()
        {
            return String.Empty;
        }
    }
}
