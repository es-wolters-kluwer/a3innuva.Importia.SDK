namespace a3innuva.TAA.Migration.SDK.Implementations
{
    using System.Collections.Generic;
    using a3innuva.TAA.Migration.SDK.Interfaces.Entities.Journal;

    public class AnalyticDistribution : IAnalyticDistribution
    {
        public IEnumerable<IAnalyticDistributionDetail> Details { get; set; }
        public decimal Percentage { get; set; }
    }
}
