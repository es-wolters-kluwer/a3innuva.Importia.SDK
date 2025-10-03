using System;

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
        [Obsolete("This property is deprecated, please use Partner property instead.")]
        string Name { get; set; }
        /// <summary>
        /// Third party fiscal document, must be informed if name or postalCode was informed [0-20]
        /// </summary>
        [Obsolete("his property is deprecated, please use Partner property instead.")]
        string VatNumber { get; set; }
        /// <summary>
        /// Third party postal code, mandatory if name and postalCode wa informed [0-5]
        /// </summary>
        [Obsolete("his property is deprecated, please use Partner property instead.")]
        string PostalCode { get; set; }
        /// <summary>
        /// Country code on format ISO 3166-1 alpha-2, optional
        /// </summary>
        [Obsolete("his property is deprecated, please use Partner property instead.")]
        string CountryCode { get; set; }
        /// <summary>
        /// Third party vatNumber type, optional [0,8]
        /// </summary>
        [Obsolete("his property is deprecated, please use Partner property instead.")]
        int VatType { get; set; }
        IPartner Partner { get; set; }
    }
}
