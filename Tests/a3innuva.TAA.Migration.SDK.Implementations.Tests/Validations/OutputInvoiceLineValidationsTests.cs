namespace a3innuva.TAA.Migration.SDK.Implementations.Tests
{
    using FluentAssertions;
    using System;
    using System.Linq;
    using a3innuva.TAA.Migration.SDK.Implementations;
    using a3innuva.TAA.Migration.SDK.Interfaces;
    using Xunit;

    [Trait("Unit test", "OutputInvoiceLineValidations")]
    public class OutputInvoiceLineValidationsTests
    {
        private OutputInvoiceLineValidation validation;

        public OutputInvoiceLineValidationsTests()
        {
            this.validation = new OutputInvoiceLineValidation();
        }

        ~OutputInvoiceLineValidationsTests()
        {
            this.validation = null;
        }

        [Fact(DisplayName = "Validate succeed")]
        public void Validate_succeed()
        {
            IOutputInvoiceLine entity = this.CreateEntity();

            var errors = this.validation.Validate(entity).ToList();

            errors.Count.Should().Be(0);
        }

        

        [Fact(DisplayName = "Validate base amount failed")]
        public void Validate_base_amount_failed()
        {
            IOutputInvoiceLine entity = this.CreateEntity();
            entity.BaseAmount = 0;

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "El campo 'Base imponible', obligatorio contenido");
        }

        [Fact(DisplayName = "Validate empty transaction failed")]
        public void Validate_empty_transaction_failed()
        {
            IOutputInvoiceLine entity = this.CreateEntity();
            entity.Transaction = String.Empty;

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "El campo 'Operación', obligatorio contenido");
        }

        [Fact(DisplayName = "Validate bad transaction failed")]
        public void Validate_bad_transaction_failed()
        {
            IOutputInvoiceLine entity = this.CreateEntity();
            entity.Transaction = "op";

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "No es una operación valida");
        }

        [Fact(DisplayName = "Validate withHolding succeed")]
        public void Validate_withHolding_succed()
        {
            IOutputInvoiceLine entity = this.CreateEntity();
            entity.WithHolding = "CMO_ORDO";

            var errors = this.validation.Validate(entity).ToList();

            errors.Count.Should().Be(0);
        }

        [Fact(DisplayName = "Validate bad withHolding failed")]
        public void Validate_bad_withHolding_failed()
        {
            IOutputInvoiceLine entity = this.CreateEntity();
            entity.WithHolding = "op";

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "No es una retención valida");
        }

        [Fact(DisplayName = "Validate bad withHolding percentage failed")]
        public void Validate_bad_withHolding_percentage_failed()
        {
            IOutputInvoiceLine entity = this.CreateEntity();
            entity.WithHoldingPercentage = 101.01M;

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "El campo 'Porcentaje de retención' tiene formato incorrecto");
        }
        
        private IOutputInvoiceLine CreateEntity()
        {
            return new OutputInvoiceLine()
            {
                Id = Guid.NewGuid(),
                Line = 1,
                BaseAmount = 1210,
                TaxAmount = 210,
                Transaction = "OP_INT"
            };
        }
    }
}
