namespace a3innuva.TAA.Migration.SDK.Interfaces
{
    public enum PaymentType
    {
        None = 0,
        Cheque = 10,
        DirectDebit = 20,
        PostalOrder = 30,
        AcceptedBillOfExchange = 40,
        UnaceptedBillOfExchange = 50,
        Cash = 60,
        PromissoryNote = 70,
        BankTransfer = 80
    }
}
