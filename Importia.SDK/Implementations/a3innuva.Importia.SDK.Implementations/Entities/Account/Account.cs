namespace a3innuva.TAA.Migration.SDK.Implementations
{
    using System;
    using a3innuva.TAA.Migration.SDK.Interfaces;

    /// <summary>
    /// Account entity
    /// </summary>
    public class Account : IAccount
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string VatNumber { get; set; }
        public string PostalCode { get; set; }
        public string CountryCode { get; set; }
        public int VatType { get; set; }
        public Guid Id { get; set; }
        public int Line { get; set; }
        public string Identity()
        {
            return this.Code;
        }
        public string Source { get; set; }
    }
}
