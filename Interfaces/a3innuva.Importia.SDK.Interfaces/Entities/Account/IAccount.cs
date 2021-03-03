namespace a3innuva.TAA.Migration.SDK.Interfaces
{
    /// <summary>
    /// Account entity
    /// </summary>
    public interface IAccount : IMigrationEntity, IMigrationSourceInfo
    {
        /// <summary>
        /// Account code, mandatory [6-20], for length lower than 6 this is a accountLevel entity
        /// </summary>
        string Code { get; set; }
        /// <summary>
        /// Account description, mandatory [1-255]
        /// </summary>
        string Description { get; set; }
        /// <summary>
        /// Third party fiscal name, must be informed if vatNumber or postalCode was informed [0-255]
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// Third party fiscal document, must be informed if name or postalCode was informed [0-20]
        /// </summary>
        string VatNumber { get; set; }
        /// <summary>
        /// Third party postal code, mandatory if name and postalCode wa informed [0-5]
        /// </summary>
        string PostalCode { get; set; }
        /// <summary>
        /// Country code on format ISO 3166-1 alpha-2, optional
        /// </summary>
        string CountryCode { get; set; }
        /// <summary>
        /// Third party vatNumber type, optional [0,8]
        /// </summary>
        int VatType { get; set; }
    }
}
