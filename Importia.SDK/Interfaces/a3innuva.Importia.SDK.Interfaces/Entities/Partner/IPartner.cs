namespace a3innuva.TAA.Migration.SDK.Interfaces
{
    public interface IPartner : IMigrationEntity
    {
        string AccountCode { get; set; }
        string TradeName { get; set; }
        int VatNumber { get; set; }
        int VatType { get; set; }
        string PostalCode { get; set; }
        string CountryCode { get; set; }
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
        ///// <summary>
        ///// Counterpart account, [6-20] 
        ///// </summary>
        //string CounterPart { get; set; }
        ///// <summary>
        ///// Tax code, optional value [General, TaxFree, Donations, SurchargeDonations]
        ///// </summary>
        //string TaxCode { get; set; }
        ///// <summary>
        ///// Transaction code. Go to valid transactions codes
        ///// </summary>
        //string Transaction { get; set; }
        ///// <summary>
        ///// WithHolding code. Go to valid withHolding codes
        ///// </summary>
        //string WithHolding { get; set; }
        //int[] Periodicity { get; set; }
        //int FirstPaymentDay { get; set; }
        //int SecondPaymentDay { get; set; }
        //PaymentType PaymentType { get; set; }
        //string BankAccount { get; set; }

    }
}
