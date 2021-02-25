namespace a3innuva.TAA.Migration.SDK.Interfaces
{
    using System.Collections.Generic;

    public interface IOutputInvoice : IInvoice
    {
        IOutputInvoiceLine[] Lines { get; set; }
    }
}
