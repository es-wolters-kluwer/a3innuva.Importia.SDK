namespace a3innuva.TAA.Migration.SDK.Interfaces
{
    using System;
    public interface IInputInvoiceAdditionalData : IMigrationEntity, IMigrationSourceInfo
    {   
        /// <summary>
        /// Description
        /// </summary>
        string Description { get; set;}
        /// <summary>
        /// Date of registry of AEAT
        /// </summary>
        DateTime RegistryDate { get; set; }
        /// <summary>
        /// Dua document id
        /// </summary>
        string DuaDocumentId { get; set; }  
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
        /// Type of document additional data
        /// </summary>
        TypeOfDocumentAdditionalDataEnum TypeOfDocument { get; set; }
        /// <summary>
        /// Fundamental additional data
        /// </summary>
        FundamentalAdditionalDataEnum Fundamental { get; set; }
    }
}
