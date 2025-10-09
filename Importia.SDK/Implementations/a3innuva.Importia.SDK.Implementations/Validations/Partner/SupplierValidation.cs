namespace a3innuva.TAA.Migration.SDK.Implementations
{
    using a3innuva.TAA.Migration.SDK.Interfaces;

    public class SupplierValidation : PartnerValidation
    {
        public SupplierValidation() : base() { }

        public override bool ValidateTransaction(string input)
        {
            if (string.IsNullOrEmpty(input))
                return false;

            return Transactions.ItExistForInput(input);
        }

        public override bool ValidateWithHolding(string input)
        {
            if (string.IsNullOrEmpty(input))
                return true;

            return WithHoldings.ItExistForInput(input);
        }
    }
}
