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
        string AccountCode { get; set; }
        
        /// <summary>
        /// Gets or sets the description of the fixed asset account.
        /// This field provides additional context for the account code.
        /// </summary>
        /// <value>
        /// The textual description of the fixed asset account.
        /// </value>
        string AccountDescription { get; set; }

        /// <summary>
        /// Gets or sets the type of good/asset.
        /// This field is mandatory.
        /// </summary>
        /// <value>
        /// The classification type of the asset.
        /// </value>
        int TypeOfGood { get; set; }
        
        /// <summary>
        /// Gets or sets the asset identifier.
        /// This field is optional.
        /// </summary>
        /// <value>
        /// A unique identifier for the asset, or <c>null</c> if not specified.
        /// </value>
        string Identifier { get; set; }
        
        /// <summary>
        /// Gets or sets the asset description.
        /// This field is mandatory.
        /// </summary>
        /// <value>
        /// A textual description of the asset.
        /// </value>
        string Description { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether this is an investment/capital asset.
        /// </summary>
        /// <value>
        /// <c>true</c> if this is a capital asset; otherwise, <c>false</c>.
        /// </value>
        bool IsCapitalAsset { get; set; }
        
        /// <summary>
        /// Gets or sets the accumulated depreciation account code.
        /// This field is optional and must be between 6 and 20 characters if specified.
        /// </summary>
        /// <value>
        /// The account code for accumulated depreciation, or <c>null</c> if not specified.
        /// </value>
        string AccumulatedDepreciationAccountCode { get; set; }

        /// <summary>
        /// Gets or sets the description of the accumulated depreciation account.
        /// This field provides additional context for the accumulated depreciation account code.
        /// </summary>
        /// <value>
        /// The textual description of the accumulated depreciation account.
        /// </value>
        string AccumulatedDepreciationAccountDescription { get; set; }

        /// <summary>
        /// Gets or sets the depreciation provision/endowment account code.
        /// This field is optional and must be between 6 and 20 characters if specified.
        /// </summary>
        /// <value>
        /// The account code for depreciation provision, or <c>null</c> if not specified.
        /// </value>
        string EndowmentAccountCode { get; set; }
        
        /// <summary>
        /// Gets or sets the description of the depreciation provision/endowment account.
        /// This field provides additional context for the endowment account code.
        /// </summary>
        /// <value>
        /// The textual description of the depreciation provision/endowment account.
        /// </value>
        string EndowmentAccountDescription { get; set; }

        // Datos de la compra / adquisición
        /// <summary>
        /// Gets or sets the acquisition date of the asset.
        /// This field is mandatory.
        /// </summary>
        /// <value>
        /// The date when the asset was acquired.
        /// </value>
        DateTime AcquisitionDate { get; set; }
        
        /// <summary>
        /// Gets or sets the acquisition value of the asset.
        /// This field is mandatory and must be in format X.XX.
        /// </summary>
        /// <value>
        /// The total value paid for acquiring the asset.
        /// </value>
        decimal AcquisitionValue { get; set; }
        
        /// <summary>
        /// Gets or sets the invoice number associated with the acquisition.
        /// This field is optional.
        /// </summary>
        /// <value>
        /// The invoice number, or <c>null</c> if not specified.
        /// </value>
        string AcquisitionInvoiceNumber { get; set; }
        
        /// <summary>
        /// Gets or sets the third party fiscal name.
        /// Must be informed if <see cref="AcquisitionVatNumber"/> or <see cref="AcquisitionPostalCode"/> is specified.
        /// Maximum length is 255 characters.
        /// </summary>
        /// <value>
        /// The legal/fiscal name of the vendor or supplier.
        /// </value>
        string AcquisitionPartnerName { get; set; }
        
        /// <summary>
        /// Gets or sets the third party fiscal document/VAT number.
        /// Must be informed if <see cref="AcquisitionPartnerName"/> or <see cref="AcquisitionPostalCode"/> is specified.
        /// Maximum length is 20 characters.
        /// </summary>
        /// <value>
        /// The VAT number or tax identification number of the vendor.
        /// </value>
        string AcquisitionVatNumber { get; set; }
        
        /// <summary>
        /// Gets or sets the third party account code.
        /// This field is mandatory and must be between 6 and 20 characters.
        /// </summary>
        /// <value>
        /// The account code for the vendor/supplier in the chart of accounts.
        /// </value>
        string AcquisitionPartnerAccount { get; set; }
        
        /// <summary>
        /// Gets or sets the third party postal code.
        /// Mandatory if <see cref="AcquisitionPartnerName"/> and <see cref="AcquisitionVatNumber"/> are informed.
        /// Maximum length is 5 characters.
        /// </summary>
        /// <value>
        /// The postal code of the vendor's address.
        /// </value>
        string AcquisitionPostalCode { get; set; }

        /// <summary>
        /// Third party vatNumber type, optional [0,8]
        /// </summary>
        int AcquisitionVatType { get; set; }

        /// <summary>
        /// Gets or sets the country code in ISO 3166-1 alpha-2 format.
        /// This field is optional.
        /// </summary>
        /// <value>
        /// A two-letter country code (e.g., "ES", "US"), or <c>null</c> if not specified.
        /// </value>
        string AcquisitionCountryCode { get; set; }
        
        /// <summary>
        /// Gets or sets the base amount for VAT calculation.
        /// This field is optional and must be in format X.XX if specified.
        /// </summary>
        /// <value>
        /// The taxable base amount, or <c>null</c> if not specified.
        /// </value>
        decimal? AcquisitionBaseAmount { get; set; }
        
        /// <summary>
        /// Gets or sets the VAT tax code.
        /// This field is optional.
        /// </summary>
        /// <value>
        /// The tax code identifier, or <c>null</c> if not specified.
        /// </value>
        string AcquisitionTaxCode { get; set; }
        
        /// <summary>
        /// Gets or sets the VAT amount.
        /// This field is optional and must be in format X.XX if specified.
        /// </summary>
        /// <value>
        /// The total VAT amount, or <c>null</c> if not specified.
        /// </value>
        decimal? AcquisitionTaxAmount { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether pro-rata should be applied.
        /// </summary>
        /// <value>
        /// <c>true</c> if pro-rata deduction rules apply; otherwise, <c>false</c>.
        /// </value>
        bool AcquisitionProrateApply { get; set; }
        
        /// <summary>
        /// Gets or sets the pro-rata percentage applied.
        /// Value must be between 0 and 100.
        /// </summary>
        /// <value>
        /// The pro-rata percentage, or <c>null</c> if not applicable.
        /// </value>
        decimal? AcquisitionProrateAmount { get; set; }
        
        /// <summary>
        /// Gets or sets the deductible VAT amount.
        /// This field is optional and must be in format X.XX if specified.
        /// </summary>
        /// <value>
        /// The amount of VAT that can be deducted, or <c>null</c> if not specified.
        /// </value>
        decimal? AcquisitionDeductibleAmount { get; set; }

        // Datos de baja
        /// <summary>
        /// Gets or sets the deregistration/retirement date of the asset.
        /// This field is optional.
        /// </summary>
        /// <value>
        /// The date when the asset was retired or deregistered, or <c>null</c> if still active.
        /// </value>
        DateTime? AssetEndDate { get; set; }
        
        /// <summary>
        /// Gets or sets the deregistration reason code.
        /// This field is optional and must be between 1 and 7 if specified.
        /// </summary>
        /// <value>
        /// The reason code for asset retirement, or <c>null</c> if not specified.
        /// </value>
        int? RetirementReason { get; set; }

        /// <summary>
        /// Gets or sets the invoice number associated with the asset retirement/disposal.
        /// This field is optional.
        /// </summary>
        /// <value>
        /// The invoice number related to the disposal transaction, or <c>null</c> if not specified.
        /// </value>
        string RetirementInvoiceNumber { get; set; }

        /// <summary>
        /// Gets or sets the disposal value of the asset at retirement.
        /// This field is optional and must be in format X.XX if specified.
        /// </summary>
        /// <value>
        /// The amount received from the disposal/sale of the asset, or <c>null</c> if not specified.
        /// </value>
        decimal? RetirementDisposalValue { get; set; }

        /// <summary>
        /// Gets or sets the base amount for VAT calculation on the retirement transaction.
        /// This field is optional and must be in format X.XX if specified.
        /// </summary>
        /// <value>
        /// The taxable base amount for the disposal, or <c>null</c> if not specified.
        /// </value>
        decimal? RetirementBaseAmount { get; set; }

        /// <summary>
        /// Gets or sets the VAT tax code applied to the retirement transaction.
        /// This field is optional.
        /// </summary>
        /// <value>
        /// The tax code identifier for the disposal, or <c>null</c> if not specified.
        /// </value>
        string RetirementTaxCode { get; set; }

        /// <summary>
        /// Gets or sets the VAT amount applied to the retirement transaction.
        /// This field is optional and must be in format X.XX if specified.
        /// </summary>
        /// <value>
        /// The total VAT amount on the disposal, or <c>null</c> if not specified.
        /// </value>
        decimal? RetirementTaxAmount { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the retirement transaction is exempt from VAT.
        /// </summary>
        /// <value>
        /// <c>true</c> if the disposal is VAT exempt; otherwise, <c>false</c>.
        /// </value>
        bool RetirementIsExempt { get; set; }

        // Datos de amortización
        /// <summary>
        /// Gets or sets the depreciation start date.
        /// This field is mandatory.
        /// </summary>
        /// <value>
        /// The date when depreciation begins for this asset.
        /// </value>
        DateTime AssetStartDate { get; set; }
        
        /// <summary>
        /// Gets or sets the residual value of the asset.
        /// This field is optional and must be in format X.XX if specified.
        /// </summary>
        /// <value>
        /// The estimated value remaining at the end of the asset's useful life, or <c>null</c> if not specified.
        /// </value>
        decimal? DepreciationResidualValue { get; set; }
        
        /// <summary>
        /// Gets or sets the number of years for accounting depreciation.
        /// This field is mandatory.
        /// </summary>
        /// <value>
        /// The useful life of the asset in years for accounting purposes.
        /// </value>
        decimal DepreciationYears { get; set; }

        /// <summary>
        /// Gets or sets the number of years for fiscal/tax depreciation.
        /// This field is mandatory.
        /// </summary>
        /// <value>
        /// The useful life of the asset in years for tax/fiscal purposes.
        /// </value>
        decimal DepreciationFiscalYears { get; set; }
        
        /// <summary>
        /// Gets or sets the collection of depreciation quotas for this asset.
        /// </summary>
        /// <value>
        /// An enumerable collection of <see cref="IFixedAssetDepreciationQuota"/> objects representing the depreciation schedule.
        /// </value>
        IEnumerable<IFixedAssetDepreciationQuota> DepreciationQuotas { get; set; }
    }
}
