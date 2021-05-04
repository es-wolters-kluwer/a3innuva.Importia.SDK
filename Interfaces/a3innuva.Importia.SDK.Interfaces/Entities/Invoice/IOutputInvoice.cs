namespace a3innuva.TAA.Migration.SDK.Interfaces
{
    public interface IOutputInvoice : IInvoice
    {
        IOutputInvoiceLine[] Lines { get; set; }
        ICharge[] Maturities { get; set; }
    }
}
