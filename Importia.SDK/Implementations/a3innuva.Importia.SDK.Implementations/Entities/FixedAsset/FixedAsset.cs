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
        public string AccountCode { get; set; }
        public int TypeOfGood { get; set; }
        public string Identifier { get; set; }
        public string Description { get; set; }
        public bool IsCapitalAsset { get; set; }
        public string AccumulatedDepreciationAccountCode { get; set; }
        public string EndowmentAccountCode { get; set; }
        public DateTime AcquisitionDate { get; set; }
        public decimal AcquisitionValue { get; set; }
        public string AcquisitionInvoiceNumber { get; set; }
        public string AcquisitionPartnerName { get; set; }
        public string AcquisitionVatNumber { get; set; }
        public string AcquisitionPartnerAccount { get; set; }
        public string AcquisitionPostalCode { get; set; }
        public string AcquisitionCountryCode { get; set; }
        public int AcquisitionVatType { get; set; }
        public decimal? AcquisitionBaseAmount { get; set; }
        public string AcquisitionTaxCode { get; set; }
        public decimal? AcquisitionTaxAmount { get; set; }
        public bool AcquisitionProrateApply { get; set; }
        public decimal? AcquisitionProrateAmount { get; set; }
        public decimal? AcquisitionDeductibleAmount { get; set; }
        public DateTime? AssetEndDate { get; set; }
        public int? RetirementReason { get; set; }
        public string RetirementInvoiceNumber { get; set; }
        public decimal? RetirementDisposalValue { get; set; }
        public decimal? RetirementBaseAmount { get; set; }
        public string RetirementTaxCode { get; set; }
        public decimal? RetirementTaxAmount { get; set; }
        public bool RetirementIsExempt { get; set; }
        public DateTime AssetStartDate { get; set; }
        public decimal? DepreciationResidualValue { get; set; }
        public int DepreciationYears { get; set; }
        public int DepreciationFiscalYears { get; set; }
        public IEnumerable<IFixedAssetDepreciationQuota> DepreciationQuotas { get; set; }
        public Guid Id { get; set; }
        public int Line { get; set; }
        public string Source { get; set; }
        public string AccountDescription { get; set; }
        public string AccumulatedDepreciationAccountDescription { get; set; }
        public string EndowmentAccountDescription { get; set; }

        public string Identity()
        {
            return this.Description;
        }
    }
}
