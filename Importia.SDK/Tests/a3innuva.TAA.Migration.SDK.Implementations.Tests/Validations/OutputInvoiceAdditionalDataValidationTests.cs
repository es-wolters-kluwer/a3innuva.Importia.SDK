﻿namespace a3innuva.TAA.Migration.SDK.Implementations.Tests
{
    using a3innuva.TAA.Migration.SDK.Interfaces;
    using FluentAssertions;
    using System;
    using System.Linq;
    using Xunit;

    [Trait("Unit test", "OutputInvoiceAdditionalDataValidation")]
    public class OutputInvoiceAdditionalDataValidationTests
    {
        private OutputInvoiceAdditionalDataValidation validation;

        public OutputInvoiceAdditionalDataValidationTests()
        {
            this.validation = new OutputInvoiceAdditionalDataValidation();
        }

        ~OutputInvoiceAdditionalDataValidationTests()
        {
            this.validation = null;
        }

        [Fact(DisplayName = "Validate succeed")]
        public void Validate_succeed()
        {
            IOutputInvoiceAdditionalData entity = this.CreateEntity();

            var errors = this.validation.Validate(entity).ToList();

            errors.Count.Should().Be(0);
        }

        [Fact(DisplayName = "Validate id failed")]
        public void Validate_id_failed()
        {
            IOutputInvoiceAdditionalData entity = this.CreateEntity();
            entity.Id = Guid.Empty;

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "Id");
        }

        [Fact(DisplayName = "Validate description length failed")]
        public void Validate_description_length_failed()
        {
            IOutputInvoiceAdditionalData entity = this.CreateEntity();
            entity.Description = "1".PadLeft(501);

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "El campo 'Descripción adicional' tiene longitud incorrecta");
        }

        [Fact(DisplayName = "Validate initial invoice number length failed")]
        public void Validate_initial_invoice_number_length_failed()
        {
            IOutputInvoiceAdditionalData entity = this.CreateEntity();
            entity.InitialNumberOfDocument = "1".PadLeft(61);

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "El campo 'Número de documento inicial' tiene longitud incorrecta");
        }

        [Fact(DisplayName = "Validate initial invoice number null succeed")]
        public void Validate_initial_invoice_number_null_succeed()
        {
            IOutputInvoiceAdditionalData entity = this.CreateEntity();
            entity.InitialNumberOfDocument = null;

            var errors = this.validation.Validate(entity).ToList();

            errors.Count.Should().Be(0);
        }

        [Fact(DisplayName = "Validate last invoice number length failed")]
        public void Validate_last_invoice_number_length_failed()
        {
            IOutputInvoiceAdditionalData entity = this.CreateEntity();
            entity.LastNumberOfDocument = "1".PadLeft(61);

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "El campo 'Número de documento final' tiene longitud incorrecta");
        }

        [Fact(DisplayName = "Validate last invoice number null succeed")]
        public void Validate_last_invoice_number_null_succeed()
        {
            IOutputInvoiceAdditionalData entity = this.CreateEntity();
            entity.LastNumberOfDocument = null;

            var errors = this.validation.Validate(entity).ToList();

            errors.Count.Should().Be(0);
        }

        [Fact(DisplayName = "Validate empty type of document succeed")]
        public void Validate_empty_typeOfDocument_succeed()
        {
            IOutputInvoiceAdditionalData entity = this.CreateEntity();
            entity.TypeOfDocument = String.Empty;

            var errors = this.validation.Validate(entity).ToList();

            errors.Count.Should().Be(0);
        }

        [Fact(DisplayName = "Validate bad type of document failed")]
        public void Validate_bad_typeOfDocument_failed()
        {
            IOutputInvoiceAdditionalData entity = this.CreateEntity();
            entity.TypeOfDocument = "op";

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "No es un tipo de documento válido");
        }

        [Fact(DisplayName = "Validate type of document succeed")]
        public void Validate_typeOfDocument_succed()
        {
            IOutputInvoiceAdditionalData entity = this.CreateEntity();
            entity.TypeOfDocument = "TIPO_FACTURA_COMPLETA";

            var errors = this.validation.Validate(entity).ToList();

            errors.Count.Should().Be(0);
        }

        [Fact(DisplayName = "Validate empty fundamental succeed")]
        public void Validate_empty_fundamental_succeed()
        {
            IOutputInvoiceAdditionalData entity = this.CreateEntity();
            entity.Fundamental = String.Empty;

            var errors = this.validation.Validate(entity).ToList();

            errors.Count.Should().Be(0);
        }

        [Fact(DisplayName = "Validate bad fundamental failed")]
        public void Validate_bad_fundamental_failed()
        {
            IOutputInvoiceAdditionalData entity = this.CreateEntity();
            entity.Fundamental = "op";

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "No es un tipo de clave válida");
        }

        [Fact(DisplayName = "Validate fundamental succeed")]
        public void Validate_fundamental_succed()
        {
            IOutputInvoiceAdditionalData entity = this.CreateEntity();
            entity.Fundamental = "CLAVE_REGIMEN_GENERAL";

            var errors = this.validation.Validate(entity).ToList();

            errors.Count.Should().Be(0);
        }

        private IOutputInvoiceAdditionalData CreateEntity()
        {
            return new OutputInvoiceAdditionalData()
            {
                Id = Guid.NewGuid(),
                Description = "desc",
                InitialNumberOfDocument = "1",
                LastNumberOfDocument = "2",
                TypeOfDocument = "TIPO_FACTURA_COMPLETA",
                Fundamental = "CLAVE_REGIMEN_GENERAL"
            };
        }
    }
}
