namespace a3innuva.TAA.Migration.SDK.Interfaces
{
    public interface IPartner : IMigrationEntity
    {

        /// <summary>
        /// Counterpart account, [6-20] 
        /// </summary>
        string CounterPart { get; set; }
        /// <summary>
        /// Tax code, optional value [General, TaxFree, Donations, SurchargeDonations]
        /// </summary>
        string TaxCode { get; set; }
        /// <summary>
        /// Transaction code. Go to valid transactions codes
        /// </summary>
        string Transaction { get; set; }
        /// <summary>
        /// WithHolding code. Go to valid withHolding codes
        /// </summary>
        string WithHolding { get; set; }
        string Periodicity { get; set; }
        int? FirstPaymentDay { get; set; }
        int? SecondPaymentDay { get; set; }
        PaymentType? PaymentType { get; set; }
        string BankAccount { get; set; }

    }
}
