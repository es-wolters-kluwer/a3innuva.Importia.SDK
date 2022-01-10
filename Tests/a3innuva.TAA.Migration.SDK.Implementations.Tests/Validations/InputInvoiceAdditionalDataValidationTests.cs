namespace a3innuva.TAA.Migration.SDK.Implementations.Tests
{
    using a3innuva.TAA.Migration.SDK.Interfaces;
    using FluentAssertions;
    using System;
    using System.Linq;
    using Xunit;

    [Trait("Unit test", "InputInvoiceAdditionalDataValidation")]
    public class InputInvoiceAdditionalDataValidationTests
    {
        private InputInvoiceAdditionalDataValidation validation;

        public InputInvoiceAdditionalDataValidationTests()
        {
            this.validation = new InputInvoiceAdditionalDataValidation();
        }

        ~InputInvoiceAdditionalDataValidationTests()
        {
            this.validation = null;
        }

        [Fact(DisplayName = "Validate succeed")]
        public void Validate_succeed()
        {
            IInputInvoiceAdditionalData entity = this.CreateEntity();

            var errors = this.validation.Validate(entity).ToList();

            errors.Count.Should().Be(0);
        }

        [Fact(DisplayName = "Validate id failed")]
        public void Validate_id_failed()
        {
            IInputInvoiceAdditionalData entity = this.CreateEntity();
            entity.Id = Guid.Empty;

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "Id");
        }

        [Fact(DisplayName = "Validate description length failed")]
        public void Validate_description_length_failed()
        {
            IInputInvoiceAdditionalData entity = this.CreateEntity();
            entity.Description = "1".PadLeft(501);

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "El campo 'Descripción' tiene longitud incorrecta");
        }

        [Fact(DisplayName = "Validate DUA length failed")]
        public void Validate_DUA_length_failed()
        {
            IInputInvoiceAdditionalData entity = this.CreateEntity();
            entity.DuaDocumentId = "1".PadLeft(19);

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "El campo 'Número de DUA' tiene longitud incorrecta");
        }

        [Fact(DisplayName = "Validate initial invoice number length failed")]
        public void Validate_initial_invoice_number_length_failed()
        {
            IInputInvoiceAdditionalData entity = this.CreateEntity();
            entity.InitialNumberOfDocument = "1".PadLeft(61);

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "El campo 'Número de documento inicial' tiene longitud incorrecta");
        }
        [Fact(DisplayName = "Validate initial invoice number null succeed")]
        public void Validate_initial_invoice_number_null_succeed()
        {
            IInputInvoiceAdditionalData entity = this.CreateEntity();
            entity.InitialNumberOfDocument = null;

            var errors = this.validation.Validate(entity).ToList();

            errors.Count.Should().Be(0);
        }

        [Fact(DisplayName = "Validate last invoice number length failed")]
        public void Validate_last_invoice_number_length_failed()
        {
            IInputInvoiceAdditionalData entity = this.CreateEntity();
            entity.LastNumberOfDocument = "1".PadLeft(61);

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "El campo 'Número de documento final' tiene longitud incorrecta");
        }

        [Fact(DisplayName = "Validate last invoice number null succeed")]
        public void Validate_last_invoice_number_null_succeed()
        {
            IInputInvoiceAdditionalData entity = this.CreateEntity();
            entity.LastNumberOfDocument = null;

            var errors = this.validation.Validate(entity).ToList();

            errors.Count.Should().Be(0);
        }

        [Fact(DisplayName = "Validate empty type of document succeed")]
        public void Validate_empty_typeOfDocument_succeed()
        {
            IInputInvoiceAdditionalData entity = this.CreateEntity();
            entity.TypeOfDocument = String.Empty;

            var errors = this.validation.Validate(entity).ToList();

            errors.Count.Should().Be(0);
        }

        [Fact(DisplayName = "Validate bad type of document failed")]
        public void Validate_bad_typeOfDocument_failed()
        {
            IInputInvoiceAdditionalData entity = this.CreateEntity();
            entity.TypeOfDocument = "op";

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "No es un tipo de documento válido");
        }

        [Fact(DisplayName = "Validate type of document succeed")]
        public void Validate_typeOfDocument_succed()
        {
            IInputInvoiceAdditionalData entity = this.CreateEntity();
            entity.TypeOfDocument = "TIPO_FACTURA_COMPLETA";

            var errors = this.validation.Validate(entity).ToList();

            errors.Count.Should().Be(0);
        }

        [Fact(DisplayName = "Validate empty fundamental succeed")]
        public void Validate_empty_fundamental_succeed()
        {
            IInputInvoiceAdditionalData entity = this.CreateEntity();
            entity.Fundamental = String.Empty;

            var errors = this.validation.Validate(entity).ToList();

            errors.Count.Should().Be(0);
        }

        [Fact(DisplayName = "Validate bad fundamental failed")]
        public void Validate_bad_fundamental_failed()
        {
            IInputInvoiceAdditionalData entity = this.CreateEntity();
            entity.Fundamental = "op";

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "No es un tipo de clave válida");
        }

        [Fact(DisplayName = "Validate fundamental succeed")]
        public void Validate_fundamental_succed()
        {
            IInputInvoiceAdditionalData entity = this.CreateEntity();
            entity.Fundamental = "CLAVE_REGIMEN_GENERAL";

            var errors = this.validation.Validate(entity).ToList();

            errors.Count.Should().Be(0);
        }

        private IInputInvoiceAdditionalData CreateEntity()
        {
            return new InputInvoiceAdditionalData()
            {
                Id = Guid.NewGuid(),
                Description = "desc",
                DuaDocumentId = "Dua",
                InitialNumberOfDocument = "1",
                LastNumberOfDocument = "2",
                TypeOfDocument = "TIPO_FACTURA_COMPLETA",
                Fundamental = "CLAVE_REGIMEN_GENERAL"
            };
        }
    }
}
