namespace a3innuva.TAA.Migration.SDK.Implementations
{
    using System;
    using System.Collections.Generic;
    using a3innuva.TAA.Migration.SDK.Interfaces;

    /// <summary>
    /// Account entity
    /// </summary>
    public class FixedAsset : IFixedAsset
    {
        public string IdentificationAccountCode { get; set; }
        public int IdentificationTypeOfGood { get; set; }
        public string IdentificationIdentifier { get; set; }
        public string IdentificationDescription { get; set; }
        public bool IdentificationIsCapitalAsset { get; set; }
        public string IdentificationAccumulatedDepreciationAccountCode { get; set; }
        public string IdentificationEndowmentAccountCode { get; set; }
        public DateTime IdentificationAcquisitionDate { get; set; }
        public decimal IdentificationAcquisitionValue { get; set; }
        public string IdentificationInvoiceNumber { get; set; }
        public string IdentificationPartnerName { get; set; }
        public string IdentificationVatNumber { get; set; }
        public string IdentificationPartnerAccount { get; set; }
        public string IdentificationPostalCode { get; set; }
        public string IdentificationCountryCode { get; set; }
        public int IdentificationVatType { get; set; }
        public decimal? IdentificationBaseAmount { get; set; }
        public string IdentificationTaxCode { get; set; }
        public decimal? IdentificationTaxAmount { get; set; }
        public bool IdentificationProrateApply { get; set; }
        public decimal? IdentificationProrateAmountValue { get; set; }
        public decimal? IdentificationDeductibleAmountValue { get; set; }
        public DateTime? IdentificationAssetEndDate { get; set; }
        public int? IdentificationRetirementReason { get; set; }
        public DateTime RepaymentDataAssetStartDate { get; set; }
        public decimal? RepaymentDataResidualValue { get; set; }
        public decimal RepaymentDataPercentageDepreciation { get; set; }
        public decimal RepaymentDataPercentageFiscalDepreciation { get; set; }
        public IEnumerable<IFixedAssetDepreciationQuota> DepreciationQuotas { get; set; }
        public Guid Id { get; set; }
        public int Line { get; set; }
        public string Source { get; set; }
        public string IdentificationAccountDescription { get; set; }
        public string IdentificationAccumulatedDepreciationAccountDescription { get; set; }
        public string IdentificationEndowmentAccountDescription { get; set; }

        public string Identity()
        {
            return this.IdentificationDescription;
        }
    }
}
