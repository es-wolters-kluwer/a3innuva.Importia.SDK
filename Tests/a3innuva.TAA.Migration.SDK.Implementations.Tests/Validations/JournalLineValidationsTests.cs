namespace a3innuva.TAA.Migration.SDK.Implementations.Tests
{
    using FluentAssertions;
    using System;
    using System.Linq;
    using a3innuva.TAA.Migration.SDK.Interfaces;
    using Xunit;

    [Trait("Unit test", "JournalLineValidations")]
    public class JournalLineValidationsTests
    {
        private JournalLineValidation validation;

        public JournalLineValidationsTests()
        {
            this.validation = new JournalLineValidation();
        }

        ~JournalLineValidationsTests()
        {
            this.validation = null;
        }

        [Fact(DisplayName = "Validate succeed")]
        public void Validate_succeed()
        {
            IJournalLine entity = this.CreateEntity();

            var errors = this.validation.Validate(entity).ToList();

            errors.Count.Should().Be(0);
        }

        [Fact(DisplayName = "Validate account required failed")]
        public void Validate_account_required_failed()
        {
            IJournalLine entity = this.CreateEntity();
            entity.Account = "";

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "El campo 'Cuenta', obligatorio contenido");
        }

        [Theory(DisplayName = "Validate account numeric failed")]
        [InlineData("000000")]
        [InlineData("A00000")]
        public void Validate_account_numeric_failed(string input)
        {
            IJournalLine entity = this.CreateEntity();
            entity.Account = input;

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "El campo 'Cuenta' tiene formato incorrecto");
        }

        [Fact(DisplayName = "Validate account length failed")]
        public void Validate_account_length_failed()
        {
            IJournalLine entity = this.CreateEntity();
            entity.Account = "123456789012345678901";

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "El campo 'Cuenta' tiene longitud incorrecta");
        }

        [Fact(DisplayName = "Validate account description required failed")]
        public void Validate_account_description_required_failed()
        {
            IJournalLine entity = this.CreateEntity();
            entity.AccountDescription = String.Empty;

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "El campo 'Descripción cuenta', obligatorio contenido");
        }

        [Fact(DisplayName = "Validate account description length failed")]
        public void Validate_account_description_length_failed()
        {
            IJournalLine entity = this.CreateEntity();
            entity.AccountDescription = @"0123456789
                                0123456789
                                0123456789
                                0123456789
                                0123456789
                                0123456789
                                0123456789
                                0123456789
                                0123456789
                                0123456789
                                0123456789
                                0123456789
                                0123456789
                                0123456789
                                0123456789
                                0123456789
                                0123456789
                                0123456789
                                0123456789
                                0123456789
                                0123456789
                                0123456789
                                0123456789
                                0123456789
                                0123456789
                                123456";

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "El campo 'Descripción cuenta' tiene longitud incorrecta");
        }

        [Fact(DisplayName = "Validate document not required succeed")]
        public void Validate_document_not_required_succeed()
        {
            IJournalLine entity = this.CreateEntity();
            entity.Document = null;

            var errors = this.validation.Validate(entity);

            errors.Count().Should().Be(0);
        }

        [Theory(DisplayName = "Validate document alphanumeric succeed")]
        [InlineData("Proveedores (euros)")]
        [InlineData("DEUDAS A LARGO PLAZO POR PRÉS")]
        [InlineData("Cliente 430")]
        [InlineData("Nuestra Fra.Nº A")]
        [InlineData("Nuestra Fra.Nº A/1")]
        [InlineData("Nuestra Fra.Nº A--1")]
        public void Validate_document_alphanumeric_succeed(string input)
        {
            IJournalLine entity = this.CreateEntity();
            entity.Document = input;

            var errors = this.validation.Validate(entity);

            errors.Count().Should().Be(0);
        }

        [Fact(DisplayName = "Validate document length failed")]
        public void Validate_document_length_failed()
        {
            IJournalLine entity = this.CreateEntity();
            entity.Document = @"0123456789
                                0123456789
                                0123456789
                                0123456789
                                0123456789
                                0123456789
                                1";

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "El campo 'Documento' tiene longitud incorrecta");
        }

        [Fact(DisplayName = "Validate concept not required succeed")]
        public void Validate_concept_not_required_succeed()
        {
            IJournalLine entity = this.CreateEntity();
            entity.Concept = "";

            var errors = this.validation.Validate(entity);

            errors.Count().Should().Be(0);
        }

        [Theory(DisplayName = "Validate concept alphanumeric succeed")]
        [InlineData("Proveedores (euros)")]
        [InlineData("DEUDAS A LARGO PLAZO POR PRÉS")]
        [InlineData("Cliente 430")]
        [InlineData("Nuestra Fra.Nº A")]
        [InlineData("Nuestra Fra.Nº A/1")]
        [InlineData("Nuestra Fra.Nº A-1")]
        public void Validate_concept_alphanumeric_succeed(string input)
        {
            IJournalLine entity = this.CreateEntity();
            entity.Concept = input;

            var errors = this.validation.Validate(entity);

            errors.Count().Should().Be(0);
        }

        [Fact(DisplayName = "Validate concept length failed")]
        public void Validate_concept_length_failed()
        {
            IJournalLine entity = this.CreateEntity();
            entity.Concept = @"0123456789
                                0123456789
                                0123456789
                                0123456789
                                0123456789
                                0123456789
                                0123456789
                                0123456789
                                0123456789
                                0123456789
                                0123456789
                                0123456789
                                0123456789
                                0123456789
                                0123456789
                                0123456789
                                0123456789
                                0123456789
                                0123456789
                                0123456789
                                0123456789
                                0123456789
                                0123456789
                                0123456789
                                0123456789
                                123456";

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "El campo 'Concepto' tiene longitud incorrecta");
        }
        
        [Fact(DisplayName = "Validate none amount failed")]
        public void Validate_none_amount_failed()
        {
            IJournalLine entity = this.CreateEntity();
            entity.Debit = 0;
            entity.Credit = 0;

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "Los importes no son correctos");
        }

        [Fact(DisplayName = "Validate two amount failed")]
        public void Validate_two_amount_failed()
        {
            IJournalLine entity = this.CreateEntity();
            entity.Debit = 1;
            entity.Credit = 1;

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "Los importes no son correctos");
        }

        private IJournalLine CreateEntity()
        {
            return new JournalLine()
            {
                Id = Guid.NewGuid(),
                Line = 1,
                Account = "43001101",
                AccountDescription = "Account description",
                Document = "document",
                Concept = "concept",
                Number = "1",
                Debit = 1000,
                Credit = 0
            };
        }
    }
}
