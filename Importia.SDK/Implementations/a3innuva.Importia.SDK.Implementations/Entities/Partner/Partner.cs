namespace a3innuva.TAA.Migration.SDK.Implementations
{
    using System;
    using a3innuva.TAA.Migration.SDK.Interfaces;

    public class Partner : IPartner
    {
        public Guid Id { get; set; }
        public int Line { get; set; }

        public string Identity()
        {
            return String.Empty;
        }
        public string TradeName { get; set; }
        public string VatNumber { get; set; }
        public string PostalCode { get; set; }
        public string CountryId { get; set; }
        public Taxation Taxation { get; set; }
        public bool HasSurchage { get; set; }
        public string CounterPartAccountCode { get; set; }
        public string TaxCode { get; set; }
        public string TransactionCode { get; set; }
        public string WithHoldingCode { get; set; }
        public int MaturitiesFirstPaymentDay { get; set; }
        public int MaturitiesSecondPaymentDay { get; set; }
        public PaymentType MaturitiesPaymentType { get; set; }
        public string MaturitiesPeriodicityDescription { get; set; }
        public int[] MaturitiesPeriodicity { get; set; }
        public string MaturitiesAccountCode { get; set; }
    }
}
