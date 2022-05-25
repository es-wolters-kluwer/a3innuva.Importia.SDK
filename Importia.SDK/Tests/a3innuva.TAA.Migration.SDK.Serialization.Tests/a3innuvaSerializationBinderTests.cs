namespace a3innuva.TAA.Migration.SDK.Serialization.Tests
{
    using FluentAssertions;
    using Xunit;

    [Trait("Unit test", "a3innuvaSerializationBinder")]

    public class a3innuvaSerializationBinderTests
    {
        private a3innuvaSerializationBinder binder;

        public a3innuvaSerializationBinderTests()
        {
            this.binder = new a3innuvaSerializationBinder();
        }
        
        ~ a3innuvaSerializationBinderTests()
        {
            this.binder = null;
        }

        [Fact(DisplayName = "Check number bindings")]
        public void Check_number_bindings()
        {
            this.binder.KnownTypes.Count.Should().Be(23);
        }
        
        [Fact(DisplayName = "Check bindings")]
        public void Check_bindings()
        {
            this.binder.KnownTypes.Should().ContainSingle(x =>
                x.FullName == typeof(a3innuva.TAA.Migration.SDK.Implementations.Account).FullName);
            this.binder.KnownTypes.Should().ContainSingle(x =>
                x.FullName == typeof(a3innuva.TAA.Migration.SDK.Implementations.InputInvoice).FullName);
            this.binder.KnownTypes.Should().ContainSingle(x =>
                x.FullName == typeof(a3innuva.TAA.Migration.SDK.Implementations.InputInvoiceLine).FullName);
            this.binder.KnownTypes.Should().ContainSingle(x =>
                x.FullName == typeof(a3innuva.TAA.Migration.SDK.Implementations.Journal).FullName);
            this.binder.KnownTypes.Should().ContainSingle(x =>
                x.FullName == typeof(a3innuva.TAA.Migration.SDK.Implementations.JournalLine).FullName);
            this.binder.KnownTypes.Should().ContainSingle(x =>
                x.FullName == typeof(a3innuva.TAA.Migration.SDK.Implementations.MigrationInfo).FullName);
            this.binder.KnownTypes.Should().ContainSingle(x =>
                x.FullName == typeof(a3innuva.TAA.Migration.SDK.Implementations.MigrationSet).FullName);
            this.binder.KnownTypes.Should().ContainSingle(x =>
                x.FullName == typeof(a3innuva.TAA.Migration.SDK.Implementations.OutputInvoice).FullName);
            this.binder.KnownTypes.Should().ContainSingle(x =>
                x.FullName == typeof(a3innuva.TAA.Migration.SDK.Implementations.OutputInvoiceLine).FullName);
            this.binder.KnownTypes.Should().ContainSingle(x =>
                x.FullName == typeof(a3innuva.TAA.Migration.SDK.Implementations.Payment).FullName);
            this.binder.KnownTypes.Should().ContainSingle(x =>
                x.FullName == typeof(a3innuva.TAA.Migration.SDK.Implementations.Charge).FullName);
            this.binder.KnownTypes.Should().ContainSingle(x =>
               x.FullName == typeof(a3innuva.TAA.Migration.SDK.Implementations.InputInvoiceAdditionalData).FullName);
            this.binder.KnownTypes.Should().ContainSingle(x =>
                x.FullName == typeof(a3innuva.TAA.Migration.SDK.Implementations.InputInvoiceAdditionalData).FullName);
            this.binder.KnownTypes.Should().ContainSingle(x =>
                x.FullName == typeof(a3innuva.TAA.Migration.SDK.Interfaces.IMigrationEntity[]).FullName);
            this.binder.KnownTypes.Should().ContainSingle(x =>
                x.FullName == typeof(a3innuva.TAA.Migration.SDK.Interfaces.IInputInvoiceLine[]).FullName);
            this.binder.KnownTypes.Should().ContainSingle(x =>
                x.FullName == typeof(a3innuva.TAA.Migration.SDK.Interfaces.IJournalLine[]).FullName);
            this.binder.KnownTypes.Should().ContainSingle(x =>
                x.FullName == typeof(a3innuva.TAA.Migration.SDK.Interfaces.IOutputInvoiceLine[]).FullName);
            this.binder.KnownTypes.Should().ContainSingle(x =>
                x.FullName == typeof(a3innuva.TAA.Migration.SDK.Interfaces.IPayment[]).FullName);
            this.binder.KnownTypes.Should().ContainSingle(x =>
                x.FullName == typeof(a3innuva.TAA.Migration.SDK.Interfaces.ICharge[]).FullName);
        }
    }
}
