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
        public string CounterPart { get; set; }
        public string TaxCode { get; set; }
        public string Transaction { get; set; }
        public string WithHolding { get; set; }
        public string Periodicity { get; set; }
        public int? FirstPaymentDay { get; set; }
        public int? SecondPaymentDay { get; set; }
        public PaymentType? PaymentType { get; set; }
        public string BankAccount { get; set; }

    }
}
