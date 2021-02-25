namespace a3innuva.TAA.Migration.SDK.Implementations.Tests
{
    using FluentAssertions;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using a3innuva.TAA.Migration.SDK.Interfaces;
    using Xunit;

    [Trait("Unit test", "JournalValidations")]
    public class JournalValidationsTests
    {
        private JournalValidation validation;

        public JournalValidationsTests()
        {
            this.validation = new JournalValidation();
        }

        ~JournalValidationsTests()
        {
            this.validation = null;
        }

        [Fact(DisplayName = "Validate succeed")]
        public void Validate_succeed()
        {
            IJournal entity = this.CreateEntity();

            var errors = this.validation.Validate(entity).ToList();

            errors.Count.Should().Be(0);
        }


        [Fact(DisplayName = "Validate document not required succeed")]
        public void Validate_document_not_required_succeed()
        {
            IJournal entity = this.CreateEntity();
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
            IJournal entity = this.CreateEntity();
            entity.Document = input;

            var errors = this.validation.Validate(entity);

            errors.Count().Should().Be(0);
        }

        [Fact(DisplayName = "Validate document length failed")]
        public void Validate_document_length_failed()
        {
            IJournal entity = this.CreateEntity();
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


        [Fact(DisplayName = "Validate number required failed")]
        public void Validate_number_required_failed()
        {
            IJournal entity = this.CreateEntity();
            entity.Number = "";

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "El campo 'Número de asiento', obligatorio contenido");
        }

        [Theory(DisplayName = "Validate number numeric succeed")]
        [InlineData("1332")]
        [InlineData("AB1")]
        [InlineData("TEST")]
        public void Validate_number_alphanumeric_succeed(string input)
        {
            IJournal entity = this.CreateEntity();
            entity.Number = input;

            var errors = this.validation.Validate(entity);

            errors.Count().Should().Be(0);
        }

        [Fact(DisplayName = "Validate number length failed")]
        public void Validate_number_length_failed()
        {
            IJournal entity = this.CreateEntity();
            entity.Number = @"0123456789
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

            errors.Should().Contain(x => !x.IsValid && x.Code == "El campo 'Número de asiento' tiene longitud incorrecta");
        }

        [Fact(DisplayName = "Validate date failed")]
        public void Validate_date_failed()
        {
            IJournal entity = this.CreateEntity();
            entity.Date = DateTime.MinValue;

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "El campo 'Fecha de asiento', obligatorio contenido");
        }
        
        [Fact(DisplayName = "Validate source required failed")]
        public void Validate_source_required_failed()
        {
            IJournal entity = this.CreateEntity();
            entity.Source = "";

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "El campo 'Origen', obligatorio contenido");
        }

        private IJournal CreateEntity()
        {
            return new Journal()
            {
                Id = Guid.NewGuid(),
                Line = 1,
                Document = "document",
                Date = DateTime.Now,
                Number = "1",
                Lines = new List<IJournalLine>(){this.CreateLineEntity()}.ToArray(),
                Source = "source"
            };
        }

        private IJournalLine CreateLineEntity()
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
                Credit = 0,
            };
        }
    }
}
