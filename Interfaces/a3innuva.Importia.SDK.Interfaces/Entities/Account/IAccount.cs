namespace a3innuva.TAA.Migration.SDK.Interfaces
{
    public interface IAccount : IMigrationEntity, IMigrationSourceInfo
    {
        string Code { get; set; }
        string Description { get; set; }
        string Name { get; set; }
        string VatNumber { get; set; }
        string PostalCode { get; set; }
        string CountryCode { get; set; }
        int VatType { get; set; }
    }
}
