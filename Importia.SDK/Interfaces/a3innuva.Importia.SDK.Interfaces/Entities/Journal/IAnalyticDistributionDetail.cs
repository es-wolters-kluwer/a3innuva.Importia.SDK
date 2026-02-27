namespace a3innuva.TAA.Migration.SDK.Interfaces.Entities.Journal
{
    /// <summary>
    /// Analytic distribution detail
    /// </summary>
    public interface IAnalyticDistributionDetail
    {
        /// <summary>
        /// Level
        /// </summary>
        string Level { get; set; }

        /// <summary>
        /// Cost Center
        /// </summary>
        string CostCenter { get; set; }
    }
}
