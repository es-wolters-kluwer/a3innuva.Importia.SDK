namespace a3innuva.TAA.Migration.SDK.Implementations.Tests
{
    using System;
    using FluentAssertions;
    using Xunit;

    [Trait("Unit test", "InputInvoiceLine")]
    public class InputInvoiceTest
    {
        [Fact(DisplayName = "Check identity succeed")]
        public void Check_identity_succeed()
        {
            DateTime param1 = DateTime.UtcNow;
            string param2 = "param2";
            string param3 = "param3";
            DateTime param4 = DateTime.UtcNow;

            var entity = new InputInvoice()
            {
                InvoiceDate = param1,
                InvoiceNumber = param2,
                PartnerAccount = param3,
                JournalDate = param4

            };

            entity.Identity().Should().Be($"{param1.ToShortDateString()}-{param4.ToShortDateString()}-{param2}-{param3}");
        }
    }
}
