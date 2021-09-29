namespace a3innuva.TAA.Migration.SDK.Implementations.Tests
{
    using FluentAssertions;
    using System;
    using System.Linq;
    using a3innuva.TAA.Migration.SDK.Implementations;
    using a3innuva.TAA.Migration.SDK.Interfaces;
    using Xunit;
    using System.Text;

    [Trait("Unit test", "PaymentValidations")]
    public class PaymentValidationsTests
    {
        private PaymentValidation validation;

        public PaymentValidationsTests()
        {
            this.validation = new PaymentValidation();
        }

        ~PaymentValidationsTests()
        {
            this.validation = null;
        }

        [Fact(DisplayName = "Validate succeed")]
        public void Validate_succeed()
        {
            IPayment entity = this.CreateEntity();

            var errors = this.validation.Validate(entity).ToList();

            errors.Count.Should().Be(0);
        }

        [Fact(DisplayName = "Validate amount failed")]
        public void Validate_amount_failed()
        {
            IPayment entity = this.CreateEntity();
            entity.Amount = 0;

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "El campo 'Importe', obligatorio contenido");
        }

        [Fact(DisplayName = "Validate date failed")]
        public void Validate_date_failed()
        {
            IPayment entity = this.CreateEntity();
            entity.Date = DateTime.MinValue;

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "El campo 'Fecha', obligatorio contenido");
        }

        [Fact(DisplayName = "Validate max date failed")]
        public void Validate_max_date_failed()
        {
            IPayment entity = this.CreateEntity();
            entity.Date = new DateTime(2101, 1, 1);

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "El campo 'Fecha', obligatorio contenido");
        }

        [Fact(DisplayName = "Validate id failed")]
        public void Validate_id_failed()
        {
            IPayment entity = this.CreateEntity();
            entity.Id = Guid.Empty;

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "Id");
        }

        [Fact(DisplayName = "Validate bank account length failed")]
        public void Validate_bank_account_length_failed()
        {
            IPayment entity = this.CreateEntity();
            entity.BankAccount = "532453245352345342534264565476435675463756785687568743565";

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "El campo 'Cuenta bancaria' tiene longitud incorrecta");
        }

        [Theory(DisplayName = "Validate bank account format failed")]
        [InlineData(" ")]
        [InlineData("3423EF345")]
        public void Validate_bank_account_format_failed(string account)
        {
            IPayment entity = this.CreateEntity();
            entity.BankAccount = account;

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "El campo 'Cuenta bancaria' tiene formato incorrecto");
        }

        [Theory(DisplayName = "Validate bank account description failed")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Validate_bank_account_description_failed(string description)
        {
            IPayment entity = this.CreateEntity();
            entity.BankAccount = "3543563";
            entity.BankAccountDescription = description;

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "El campo 'Descripción de cuenta bancaria', obligatorio contenido");
        }

        [Fact(DisplayName = "Validate bank account description length failed")]
        public void Validate_bank_account_description_length_failed()
        {
            IPayment entity = this.CreateEntity();
            entity.BankAccount = "3543563";
            var sb = new StringBuilder();
            entity.BankAccountDescription = new StringBuilder().Insert(0, "a", 256).ToString();

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "El campo 'Descripción de cuenta bancaria' tiene longitud incorrecta");
        }

        [Theory(DisplayName = "Validate accounting affect")]
        [InlineData(false, PaymentSituation.Pending, "4234567890", true)]
        [InlineData(true, PaymentSituation.Pending, "4234567890", false)]
        [InlineData(false, PaymentSituation.Satisfied, "4234567890", true)]
        [InlineData(true, PaymentSituation.Satisfied, "4234567890", true)]
        [InlineData(false, PaymentSituation.Pending, null, true)]
        [InlineData(true, PaymentSituation.Pending, null, false)]
        [InlineData(false, PaymentSituation.Satisfied, null, true)]
        [InlineData(true, PaymentSituation.Satisfied, null, false)]
        public void Validate_accounting_affect(bool accountingAffect, PaymentSituation situation, string bankAccount, bool isValid)
        {
            IPayment entity = this.CreateEntity();
            entity.AccountingAffect = accountingAffect;
            entity.Situation = situation;
            entity.BankAccount = bankAccount;
            entity.BankAccountDescription = "Dummy";

            var errors = this.validation.Validate(entity).ToList();

            if (isValid)
            {
                errors.Count.Should().Be(0);
            }
            else
            {
                errors.Should().Contain(x => !x.IsValid && x.Code == "El campo 'Efecto contable' tiene valor incorrecto");
            }
        }

        private Payment CreateEntity()
        {
            return new Payment()
            {
                Id = Guid.NewGuid(),
                Line = 1,
                Amount = 1210,
                Date = DateTime.UtcNow
            };
        }
    }
}
