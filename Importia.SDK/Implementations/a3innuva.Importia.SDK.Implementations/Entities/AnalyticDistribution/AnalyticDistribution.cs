namespace a3innuva.TAA.Migration.SDK.Implementations
{
    using a3innuva.TAA.Migration.SDK.Interfaces;
    using System.Collections.Generic;

    public class AnalyticDistribution : IAnalyticDistribution
    {
        public IEnumerable<IAnalyticDistributionDetail> Details { get; set; }
        public decimal Percentage { get; set; }
    }
}
