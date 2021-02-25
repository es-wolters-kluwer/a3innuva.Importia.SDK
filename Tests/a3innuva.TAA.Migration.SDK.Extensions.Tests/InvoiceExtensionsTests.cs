namespace a3innuva.TAA.Migration.SDK.Extensions.Tests
{
    using FluentAssertions;
    using Implementations;
    using Interfaces;
    using Xunit;

    [Trait("Unit test", "InvoiceExtensions")]
    public class InvoiceExtensionsTests
    {
        [Fact(DisplayName = "HasVatNumber false")]
        public void HasVatNumber_false()
        {
            var entity = this.GetEntity();

            entity.HasVatNumber().Should().BeFalse();
        }

        [Fact(DisplayName = "HasVatNumber true")]
        public void HasVatNumber_true()
        {
            var entity = this.GetEntity();
            entity.VatNumber = "vatNumber";

            entity.HasVatNumber().Should().BeTrue();
        }

        [Fact(DisplayName = "HasPostalCode false")]
        public void HasPostalCode_false()
        {
            var entity = this.GetEntity();

            entity.HasPostalCode().Should().BeFalse();
        }

        [Fact(DisplayName = "HasPostalCode true")]
        public void HasPostalCode_true()
        {
            var entity = this.GetEntity();
            entity.PostalCode = "08000";

            entity.HasPostalCode().Should().BeTrue();
        }
        
        private IOutputInvoice GetEntity()
        {
            return new OutputInvoice();
        }
    }
}
