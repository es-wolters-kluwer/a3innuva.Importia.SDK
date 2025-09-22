namespace a3innuva.TAA.Migration.SDK.Implementations.Tests
{
    using FluentAssertions;
    using System;
    using System.Linq;
    using a3innuva.TAA.Migration.SDK.Interfaces;
    using Xunit;

    [Trait("Unit test", "PartnerValidations")]
    public class PartnerValidationsTests
    {
        private PartnerValidation validation;

        public PartnerValidationsTests()
        {
            this.validation = new PartnerValidation();
        }

        ~PartnerValidationsTests()
        {
            this.validation = null;
        }

        [Fact(DisplayName = "Validate succeed")]
        public void Validate_succeed()
        {
            IPartner entity = this.CreateEntity();

            var errors = this.validation.Validate(entity).ToList();

            errors.Count.Should().Be(0);
        }

        [Theory(DisplayName = "Validate bank account numeric failed")]
        [InlineData("000000")]
        [InlineData("A00000")]
        public void Validate_bank_account_numeric_failed(string input)
        {
            IPartner entity = this.CreateEntity();
            entity.MaturitiesAccountCode = input;

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "El campo 'Cuenta bancaria' tiene formato incorrecto");
        }

        [Fact(DisplayName = "Validate bank account length failed")]
        public void Validate_bank_account_length_failed()
        {
            IPartner entity = this.CreateEntity();
            entity.MaturitiesAccountCode = "123456789012345678901";

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "El campo 'Cuenta bancaria' tiene longitud incorrecta");
        }

        [Fact(DisplayName = "Validate empty transaction failed")]
        public void Validate_empty_transaction_failed()
        {
            IPartner entity = this.CreateEntity();
            entity.TransactionCode = String.Empty;

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "El campo 'Operación', obligatorio contenido");
        }

        [Fact(DisplayName = "Validate bad transaction failed")]
        public void Validate_bad_transaction_failed()
        {
            IPartner entity = this.CreateEntity();
            entity.TransactionCode = "op";

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "No es una operación valida");
        }

        [Fact(DisplayName = "Validate withHolding succeed")]
        public void Validate_withHolding_succed()
        {
            IPartner entity = this.CreateEntity();
            entity.WithHoldingCode = "CMO_ORDO";

            var errors = this.validation.Validate(entity).ToList();

            errors.Count.Should().Be(0);
        }


        [Theory(DisplayName = "Validate account numeric failed")]
        [InlineData("000000")]
        [InlineData("A00000")]
        public void Validate_account_numeric_failed(string input)
        {
            IPartner entity = this.CreateEntity();
            entity.CounterPartAccountCode = input;

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "El campo 'Contrapartida' tiene formato incorrecto");
        }

        [Fact(DisplayName = "Validate account length failed")]
        public void Validate_account_length_failed()
        {
            IPartner entity = this.CreateEntity();
            entity.CounterPartAccountCode = "123456789012345678901";

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "El campo 'Contrapartida' tiene longitud incorrecta");
        }

        private IPartner CreateEntity()
        {
            return new Partner()
            {
                Id = Guid.NewGuid(),
                MaturitiesAccountCode = "43001101",
                TransactionCode = "OP_INT",
                CounterPartAccountCode = "77000000"
            };
        }
    }
}
