namespace a3innuva.TAA.Migration.SDK.Implementations.Tests
{
    using FluentAssertions;
    using System;
    using System.Linq;
    using a3innuva.TAA.Migration.SDK.Implementations;
    using a3innuva.TAA.Migration.SDK.Interfaces;
    using Xunit;

    [Trait("Unit test", "ChargeValidations")]
    public class ChargeValidationsTests
    {
        private ChargeValidation validation;

        public ChargeValidationsTests()
        {
            this.validation = new ChargeValidation();
        }

        ~ChargeValidationsTests()
        {
            this.validation = null;
        }

        [Fact(DisplayName = "Validate succeed")]
        public void Validate_succeed()
        {
            ICharge entity = this.CreateEntity();

            var errors = this.validation.Validate(entity).ToList();

            errors.Count.Should().Be(0);
        }

        [Fact(DisplayName = "Validate amount failed")]
        public void Validate_amount_failed()
        {
            ICharge entity = this.CreateEntity();
            entity.Amount = 0;

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "El campo 'Importe', obligatorio contenido");
        }

        [Fact(DisplayName = "Validate date failed")]
        public void Validate_date_failed()
        {
            ICharge entity = this.CreateEntity();
            entity.Date = DateTime.MinValue;

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "El campo 'Fecha', obligatorio contenido");
        }

        [Fact(DisplayName = "Validate id failed")]
        public void Validate_id_failed()
        {
            ICharge entity = this.CreateEntity();
            entity.Id = Guid.Empty;

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "Id");
        }

        [Fact(DisplayName = "Validate bank account length failed")]
        public void Validate_bank_account_length_failed()
        {
            ICharge entity = this.CreateEntity();
            entity.BankAccount = "532453245352345342534264565476435675463756785687568743565";

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "El campo 'Cuenta bancaria' tiene longitud incorrecta");
        }

        [Theory(DisplayName = "Validate bank account format failed")]
        [InlineData(" ")]
        [InlineData("3423EF345")]
        public void Validate_bank_account_format_failed(string account)
        {
            ICharge entity = this.CreateEntity();
            entity.BankAccount = account;

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "El campo 'Cuenta bancaria' tiene formato incorrecto");
        }

        private Charge CreateEntity()
        {
            return new Charge()
            {
                Id = Guid.NewGuid(),
                Line = 1,
                Amount = 1210,
                Date = DateTime.UtcNow
            };
        }
    }
}
