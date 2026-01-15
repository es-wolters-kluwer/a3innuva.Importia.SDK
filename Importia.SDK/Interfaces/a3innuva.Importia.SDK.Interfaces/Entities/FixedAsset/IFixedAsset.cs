using System;
using System.Collections;
using System.Collections.Generic;

namespace a3innuva.TAA.Migration.SDK.Interfaces
{
    /// <summary>
    /// Fixed Asset entity
    /// </summary>
    public interface IFixedAsset : IMigrationEntity, IMigrationSourceInfo
    {
        // Datos del activo
        /// <summary>
        /// Gets or sets the fixed asset account code.
        /// This field is mandatory and must be between 6 and 20 characters.
        /// </summary>
        /// <value>
        /// The account code identifying the fixed asset in the chart of accounts.
        /// </value>
        string IdentificationAccountCode { get; set; }
        
        /// <summary>
        /// Gets or sets the description of the fixed asset account.
        /// This field provides additional context for the account code.
        /// </summary>
        /// <value>
        /// The textual description of the fixed asset account.
        /// </value>
        string IdentificationAccountDescription { get; set; }

        /// <summary>
        /// Gets or sets the type of good/asset.
        /// This field is mandatory.
        /// </summary>
        /// <value>
        /// The classification type of the asset.
        /// </value>
        int IdentificationTypeOfGood { get; set; }
        
        /// <summary>
        /// Gets or sets the asset identifier.
        /// This field is optional.
        /// </summary>
        /// <value>
        /// A unique identifier for the asset, or <c>null</c> if not specified.
        /// </value>
        string IdentificationIdentifier { get; set; }
        
        /// <summary>
        /// Gets or sets the asset description.
        /// This field is mandatory.
        /// </summary>
        /// <value>
        /// A textual description of the asset.
        /// </value>
        string IdentificationDescription { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether this is an investment/capital asset.
        /// </summary>
        /// <value>
        /// <c>true</c> if this is a capital asset; otherwise, <c>false</c>.
        /// </value>
        bool IdentificationIsCapitalAsset { get; set; }
        
        /// <summary>
        /// Gets or sets the accumulated depreciation account code.
        /// This field is optional and must be between 6 and 20 characters if specified.
        /// </summary>
        /// <value>
        /// The account code for accumulated depreciation, or <c>null</c> if not specified.
        /// </value>
        string IdentificationAccumulatedDepreciationAccountCode { get; set; }

        /// <summary>
        /// Gets or sets the description of the accumulated depreciation account.
        /// This field provides additional context for the accumulated depreciation account code.
        /// </summary>
        /// <value>
        /// The textual description of the accumulated depreciation account.
        /// </value>
        string IdentificationAccumulatedDepreciationAccountDescription { get; set; }

        /// <summary>
        /// Gets or sets the depreciation provision/endowment account code.
        /// This field is optional and must be between 6 and 20 characters if specified.
        /// </summary>
        /// <value>
        /// The account code for depreciation provision, or <c>null</c> if not specified.
        /// </value>
        string IdentificationEndowmentAccountCode { get; set; }
        
        /// <summary>
        /// Gets or sets the description of the depreciation provision/endowment account.
        /// This field provides additional context for the endowment account code.
        /// </summary>
        /// <value>
        /// The textual description of the depreciation provision/endowment account.
        /// </value>
        string IdentificationEndowmentAccountDescription { get; set; }

        // Datos de la compra / adquisición
        /// <summary>
        /// Gets or sets the acquisition date of the asset.
        /// This field is mandatory.
        /// </summary>
        /// <value>
        /// The date when the asset was acquired.
        /// </value>
        DateTime IdentificationAcquisitionDate { get; set; }
        
        /// <summary>
        /// Gets or sets the acquisition value of the asset.
        /// This field is mandatory and must be in format X.XX.
        /// </summary>
        /// <value>
        /// The total value paid for acquiring the asset.
        /// </value>
        decimal IdentificationAcquisitionValue { get; set; }
        
        /// <summary>
        /// Gets or sets the invoice number associated with the acquisition.
        /// This field is optional.
        /// </summary>
        /// <value>
        /// The invoice number, or <c>null</c> if not specified.
        /// </value>
        string IdentificationInvoiceNumber { get; set; }
        
        /// <summary>
        /// Gets or sets the third party fiscal name.
        /// Must be informed if <see cref="IdentificationVatNumber"/> or <see cref="IdentificationPostalCode"/> is specified.
        /// Maximum length is 255 characters.
        /// </summary>
        /// <value>
        /// The legal/fiscal name of the vendor or supplier.
        /// </value>
        string IdentificationPartnerName { get; set; }
        
        /// <summary>
        /// Gets or sets the third party fiscal document/VAT number.
        /// Must be informed if <see cref="IdentificationPartnerName"/> or <see cref="IdentificationPostalCode"/> is specified.
        /// Maximum length is 20 characters.
        /// </summary>
        /// <value>
        /// The VAT number or tax identification number of the vendor.
        /// </value>
        string IdentificationVatNumber { get; set; }
        
        /// <summary>
        /// Gets or sets the third party account code.
        /// This field is mandatory and must be between 6 and 20 characters.
        /// </summary>
        /// <value>
        /// The account code for the vendor/supplier in the chart of accounts.
        /// </value>
        string IdentificationPartnerAccount { get; set; }
        
        /// <summary>
        /// Gets or sets the third party postal code.
        /// Mandatory if <see cref="IdentificationPartnerName"/> and <see cref="IdentificationVatNumber"/> are informed.
        /// Maximum length is 5 characters.
        /// </summary>
        /// <value>
        /// The postal code of the vendor's address.
        /// </value>
        string IdentificationPostalCode { get; set; }

        /// <summary>
        /// Third party vatNumber type, optional [0,8]
        /// </summary>
        int IdentificationVatType { get; set; }

        /// <summary>
        /// Gets or sets the country code in ISO 3166-1 alpha-2 format.
        /// This field is optional.
        /// </summary>
        /// <value>
        /// A two-letter country code (e.g., "ES", "US"), or <c>null</c> if not specified.
        /// </value>
        string IdentificationCountryCode { get; set; }
        
        /// <summary>
        /// Gets or sets the base amount for VAT calculation.
        /// This field is optional and must be in format X.XX if specified.
        /// </summary>
        /// <value>
        /// The taxable base amount, or <c>null</c> if not specified.
        /// </value>
        decimal? IdentificationBaseAmount { get; set; }
        
        /// <summary>
        /// Gets or sets the VAT tax code.
        /// This field is optional.
        /// </summary>
        /// <value>
        /// The tax code identifier, or <c>null</c> if not specified.
        /// </value>
        string IdentificationTaxCode { get; set; }
        
        /// <summary>
        /// Gets or sets the VAT amount.
        /// This field is optional and must be in format X.XX if specified.
        /// </summary>
        /// <value>
        /// The total VAT amount, or <c>null</c> if not specified.
        /// </value>
        decimal? IdentificationTaxAmount { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether pro-rata should be applied.
        /// </summary>
        /// <value>
        /// <c>true</c> if pro-rata deduction rules apply; otherwise, <c>false</c>.
        /// </value>
        bool IdentificationProrateApply { get; set; }
        
        /// <summary>
        /// Gets or sets the pro-rata percentage applied.
        /// Value must be between 0 and 100.
        /// </summary>
        /// <value>
        /// The pro-rata percentage, or <c>null</c> if not applicable.
        /// </value>
        decimal? IdentificationProrateAmountValue { get; set; }
        
        /// <summary>
        /// Gets or sets the deductible VAT amount.
        /// This field is optional and must be in format X.XX if specified.
        /// </summary>
        /// <value>
        /// The amount of VAT that can be deducted, or <c>null</c> if not specified.
        /// </value>
        decimal? IdentificationDeductibleAmountValue { get; set; }

        // Datos de baja
        /// <summary>
        /// Gets or sets the deregistration/retirement date of the asset.
        /// This field is optional.
        /// </summary>
        /// <value>
        /// The date when the asset was retired or deregistered, or <c>null</c> if still active.
        /// </value>
        DateTime? IdentificationAssetEndDate { get; set; }
        
        /// <summary>
        /// Gets or sets the deregistration reason code.
        /// This field is optional and must be between 1 and 7 if specified.
        /// </summary>
        /// <value>
        /// The reason code for asset retirement, or <c>null</c> if not specified.
        /// </value>
        int? IdentificationRetirementReason { get; set; }

        // Datos de amortización
        /// <summary>
        /// Gets or sets the depreciation start date.
        /// This field is mandatory.
        /// </summary>
        /// <value>
        /// The date when depreciation begins for this asset.
        /// </value>
        DateTime RepaymentDataAssetStartDate { get; set; }
        
        /// <summary>
        /// Gets or sets the residual value of the asset.
        /// This field is optional and must be in format X.XX if specified.
        /// </summary>
        /// <value>
        /// The estimated value remaining at the end of the asset's useful life, or <c>null</c> if not specified.
        /// </value>
        decimal? RepaymentDataResidualValue { get; set; }
        
        /// <summary>
        /// Gets or sets the accounting depreciation percentage or useful life.
        /// This field is mandatory and must be between 0 and 100.
        /// </summary>
        /// <value>
        /// The annual depreciation percentage for accounting purposes.
        /// </value>
        decimal RepaymentDataPercentageDepreciation { get; set; }
        
        /// <summary>
        /// Gets or sets the tax depreciation percentage or useful life.
        /// This field is mandatory and must be between 0 and 100.
        /// </summary>
        /// <value>
        /// The annual depreciation percentage for tax purposes.
        /// </value>
        decimal RepaymentDataPercentageFiscalDepreciation { get; set; }
        
        /// <summary>
        /// Gets or sets the collection of depreciation quotas for this asset.
        /// </summary>
        /// <value>
        /// An enumerable collection of <see cref="IFixedAssetDepreciationQuota"/> objects representing the depreciation schedule.
        /// </value>
        IEnumerable<IFixedAssetDepreciationQuota> DepreciationQuotas { get; set; }
    }
}
