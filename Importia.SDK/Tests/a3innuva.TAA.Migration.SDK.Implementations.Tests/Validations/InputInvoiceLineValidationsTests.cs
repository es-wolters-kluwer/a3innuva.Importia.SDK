namespace a3innuva.TAA.Migration.SDK.Implementations.Tests
{
    using FluentAssertions;
    using System;
    using System.Linq;
    using a3innuva.TAA.Migration.SDK.Implementations;
    using a3innuva.TAA.Migration.SDK.Interfaces;
    using Xunit;

    [Trait("Unit test", "InputInvoiceLineValidations")]
    public class InputInvoiceLineValidationsTests
    {
        private InputInvoiceLineValidation validation;

        public InputInvoiceLineValidationsTests()
        {
            this.validation = new InputInvoiceLineValidation();
        }

        ~InputInvoiceLineValidationsTests()
        {
            this.validation = null;
        }

        [Fact(DisplayName = "Validate succeed")]
        public void Validate_succeed()
        {
            IInputInvoiceLine entity = this.CreateEntity();

            var errors = this.validation.Validate(entity).ToList();

            errors.Count.Should().Be(0);
        }

        [Fact(DisplayName = "Validate empty transaction failed")]
        public void Validate_empty_transaction_failed()
        {
            IInputInvoiceLine entity = this.CreateEntity();
            entity.Transaction = String.Empty;

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "El campo 'Operación', obligatorio contenido");
        }

        [Fact(DisplayName = "Validate bad transaction failed")]
        public void Validate_bad_transaction_failed()
        {
            IInputInvoiceLine entity = this.CreateEntity();
            entity.Transaction = "op";

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "No es una operación valida");
        }

        [Fact(DisplayName = "Validate withHolding succeed")]
        public void Validate_withHolding_succed()
        {
            IInputInvoiceLine entity = this.CreateEntity();
            entity.WithHolding = "CMO_ORDO";

            var errors = this.validation.Validate(entity).ToList();

            errors.Count.Should().Be(0);
        }

        [Fact(DisplayName = "Validate bad withHolding failed")]
        public void Validate_bad_withHolding_failed()
        {
            IInputInvoiceLine entity = this.CreateEntity();
            entity.WithHolding = "op";

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "No es una retención valida");
        }

        [Fact(DisplayName = "Validate bad withHolding percentage failed")]
        public void Validate_bad_withHolding_percentage_failed()
        {
            IInputInvoiceLine entity = this.CreateEntity();
            entity.WithHoldingPercentage = 101.01M;

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "El campo 'Porcentaje de retención' tiene formato incorrecto");
        }

        [Theory(DisplayName = "Validate account numeric failed")]
        [InlineData("000000")]
        [InlineData("A00000")]
        public void Validate_account_numeric_failed(string input)
        {
            IInputInvoiceLine entity = this.CreateEntity();
            entity.CounterPart = input;

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "El campo 'Contrapartida' tiene formato incorrecto");
        }

        [Fact(DisplayName = "Validate account length failed")]
        public void Validate_account_length_failed()
        {
            IInputInvoiceLine entity = this.CreateEntity();
            entity.CounterPart = "123456789012345678901";

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "El campo 'Contrapartida' tiene longitud incorrecta");
        }

        [Fact(DisplayName = "Validate Description length succeed")]
        public void Validate_Description_length_succeed()
        {
            IInputInvoiceLine entity = this.CreateEntity();
            entity.CounterPartDescription = "1234";

            var errors = this.validation.Validate(entity);

            errors.ToList().Count.Should().Be(0);
        }

        [Fact(DisplayName = "Validate Description length failed")]
        public void Validate_Description_length_failed()
        {
            IInputInvoiceLine entity = this.CreateEntity();
            entity.CounterPartDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce interdum gravida dolor, vehicula lobortis turpis tristique rhoncus. Donec sit amet fringilla sapien. Interdum et malesuada fames ac ante ipsum primis in faucibus. Nam sodales nisl id augue sed.";

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "El campo 'Descripción contrapartida' tiene longitud incorrecta");
        }

        private IInputInvoiceLine CreateEntity()
        {
            return new InputInvoiceLine()
            {
                Id = Guid.NewGuid(),
                Line = 1,
                BaseAmount = 1210,
                TaxAmount = 210,
                Transaction = "OP_INT",
                CounterPart = "77000000"
            };
        }
    }
}
