namespace a3innuva.TAA.Migration.SDK.Interfaces
{
    using System;
    public interface IOutputInvoiceAdditionalData : IMigrationEntity, IMigrationSourceInfo
    {   
        /// <summary>
        /// Description
        /// </summary>
        string Description { get; set;}
        /// <summary>
        /// Invoice by third
        /// </summary>
        bool InvoiceByThird { get; set; }
        /// <summary>
        /// Multiple recipients
        /// </summary>
        bool MultipleRecipients { get; set; }
        /// <summary>
        /// Coupons, bonuses or discounts
        /// </summary>
        bool CouponsBonusesOrDiscounts { get; set; }
        /// <summary>
        /// Invoice in special regime
        /// </summary>
        bool InvoicesInSpecialRegime { get; set; }
        /// <summary>
        /// Taxable income at cost
        /// </summary>
        decimal TaxableIncomeAtCost { get; set; }
        /// <summary>
        /// Initial number of document
        /// </summary>
        string InitialNumberOfDocument { get; set; }
        /// <summary>
        /// Last number of document
        /// </summary>
        string LastNumberOfDocument { get; set; }
        /// <summary>
        /// Not included in census
        /// </summary>
        bool NotIncludedInCensus { get; set; }
        /// <summary>
        /// Type of document additional data
        /// </summary>
        TypeOfDocumentAdditionalDataEnum TypeOfDocument { get; set; }
        /// <summary>
        /// Fundamental additional data
        /// </summary>
        FundamentalAdditionalDataEnum Fundamental { get; set; }
    }
}
