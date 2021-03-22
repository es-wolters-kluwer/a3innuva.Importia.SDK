namespace a3innuva.TAA.Migration.SDK.Interfaces
{
    public interface IInputInvoice : IInvoice
    {
        IInputInvoiceLine[] Lines { get; set; }
        IPayment[] Maturities { get; set; }
    }
}
