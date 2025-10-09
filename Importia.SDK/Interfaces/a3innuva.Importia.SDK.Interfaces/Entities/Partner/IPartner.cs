namespace a3innuva.TAA.Migration.SDK.Interfaces
{
    public interface IPartner : IMigrationEntity
    {
        int VatType { get; set; }
        string Name { get; set; }
        string VatNumber { get; set; }
        string PostalCode { get; set; }
        string CountryCode { get; set; }
        Taxation Taxation { get; set; }
        bool HasSurchage { get; set; }
        string CounterPartAccountCode { get; set; }
        string TaxCode { get; set; }
        string TransactionCode { get; set; }
        string WithHoldingCode { get; set; }
        int MaturitiesFirstPaymentDay { get; set; }
        int MaturitiesSecondPaymentDay { get; set; }
        PaymentType MaturitiesPaymentType { get; set; }
        string MaturitiesPeriodicityDescription { get; set; }
        int[] MaturitiesPeriodicity { get; set; }
        string MaturitiesAccountCode { get; set; }
    }
}