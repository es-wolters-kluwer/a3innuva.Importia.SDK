namespace a3innuva.TAA.Migration.SDK.Interfaces
{
    using System.Collections.Generic;

    /// <summary>
    /// Analytic distribution
    /// </summary>
    public interface IAnalyticDistribution
    {
        /// <summary>
        /// Analytic distribution details
        /// </summary>
        IEnumerable<IAnalyticDistributionDetail> Details { get; set; }

        /// <summary>
        /// Percentage
        /// </summary>
        decimal Percentage { get; set; }
    }
}
