using System;

namespace a3innuva.TAA.Migration.SDK.Interfaces
{
    /// <summary>
    /// Represents a depreciation quota for a fixed asset, containing both accounting and fiscal depreciation amounts for a specific period.
    /// </summary>
    public interface IDepreciationQuota : IMigrationEntity
    {
        /// <summary>
        /// Gets or sets the accounting year for which this depreciation quota applies.
        /// </summary>
        /// <value>The accounting year as an integer (e.g., 2024).</value>
        int AccountingYear { get; set; }

        /// <summary>
        /// Gets or sets the accounting depreciation quota amount.
        /// </summary>
        /// <value>The depreciation amount used for accounting purposes.</value>
        decimal AccountingQuotaAmount { get; set; }

        /// <summary>
        /// Gets or sets the fiscal depreciation quota amount.
        /// </summary>
        /// <value>The depreciation amount used for fiscal/tax purposes.</value>
        decimal FiscalQuotaAmount { get; set; }

        /// <summary>
        /// Gets or sets the start date of the depreciation period.
        /// </summary>
        /// <value>The start date of the period, or <c>null</c> if not specified.</value>
        DateTime? StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date of the depreciation period.
        /// </summary>
        /// <value>The end date of the period, or <c>null</c> if not specified.</value>
        DateTime? EndDate { get; set; }
    }
}
