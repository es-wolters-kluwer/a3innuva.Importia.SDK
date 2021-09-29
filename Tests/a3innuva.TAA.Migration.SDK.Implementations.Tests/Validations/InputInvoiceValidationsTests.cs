namespace a3innuva.TAA.Migration.SDK.Implementations.Tests
{
    using FluentAssertions;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using a3innuva.TAA.Migration.SDK.Implementations;
    using a3innuva.TAA.Migration.SDK.Interfaces;
    using Xunit;

    [Trait("Unit test", "InputInvoiceValidations")]
    public class InputInvoiceValidationsTests
    {
        private InputInvoiceValidation validation;

        public InputInvoiceValidationsTests()
        {
            this.validation = new InputInvoiceValidation();
        }

        ~InputInvoiceValidationsTests()
        {
            this.validation = null;
        }

        [Fact(DisplayName = "Validate succeed")]
        public void Validate_succeed()
        {
            IInputInvoice entity = this.CreateEntity();

            var errors = this.validation.Validate(entity).ToList();

            errors.Count.Should().Be(0);
        }

        [Fact(DisplayName = "Validate invoice date failed")]
        public void Validate_invoice_date_failed()
        {
            IInputInvoice entity = this.CreateEntity();
            entity.InvoiceDate = DateTime.MinValue;

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "El campo 'Fecha de factura', obligatorio contenido");
        }

        [Fact(DisplayName = "Validate invoice max date failed")]
        public void Validate_invoice_max_date_failed()
        {
            IInputInvoice entity = this.CreateEntity();
            entity.InvoiceDate = new DateTime(2101, 1, 1);

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "El campo 'Fecha de factura', obligatorio contenido");
        }

        [Fact(DisplayName = "Validate journal date failed")]
        public void Validate_journal_date_failed()
        {
            IInputInvoice entity = this.CreateEntity();
            entity.JournalDate = DateTime.MinValue;

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "El campo 'Fecha de asiento' tiene formato incorrecto");
        }

        [Fact(DisplayName = "Validate journal date failed")]
        public void Validate_journal_max_date_failed()
        {
            IInputInvoice entity = this.CreateEntity();
            entity.JournalDate = new DateTime(2101, 1, 1);

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "El campo 'Fecha de asiento' tiene formato incorrecto");
        }

        [Fact(DisplayName = "Validate transaction date failed")]
        public void Validate_transaction_date_failed()
        {
            IInputInvoice entity = this.CreateEntity();
            entity.TransactionDate = DateTime.MinValue;

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "El campo 'Fecha de operación' tiene formato incorrecto");
        }

        [Fact(DisplayName = "Validate transaction max date failed")]
        public void Validate_transaction_max_date_failed()
        {
            IInputInvoice entity = this.CreateEntity();
            entity.TransactionDate = new DateTime(2101, 1, 1);

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "El campo 'Fecha de operación' tiene formato incorrecto");
        }

        [Fact(DisplayName = "Validate account required failed")]
        public void Validate_account_required_failed()
        {
            IInputInvoice entity = this.CreateEntity();
            entity.PartnerAccount = "";

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "El campo 'Cuenta de cliente', obligatorio contenido");
        }

        [Theory(DisplayName = "Validate account numeric failed")]
        [InlineData("000000")]
        [InlineData("A00000")]
        public void Validate_account_numeric_failed(string input)
        {
            IInputInvoice entity = this.CreateEntity();
            entity.PartnerAccount = input;

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "El campo 'Cuenta de cliente' tiene formato incorrecto");
        }

        [Fact(DisplayName = "Validate account length failed")]
        public void Validate_account_length_failed()
        {
            IInputInvoice entity = this.CreateEntity();
            entity.PartnerAccount = "123456789012345678901";

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "El campo 'Cuenta de cliente' tiene longitud incorrecta");
        }


        [Fact(DisplayName = "Validate invoice number required failed")]
        public void Validate_invoice_number_required_failed()
        {
            IInputInvoice entity = this.CreateEntity();
            entity.InvoiceNumber = "";

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "El campo 'Número de factura', obligatorio contenido");
        }

        [Fact(DisplayName = "Validate invoice number required failed")]
        public void Validate_invoice_number_length_failed()
        {
            IInputInvoice entity = this.CreateEntity();
            entity.InvoiceNumber = @"0123456789
                                0123456789
                                0123456789
                                0123456789
                                0123456789
                                0123456789
                                0";

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "El campo 'Número de factura' tiene longitud incorrecta");
        }

        [Theory(DisplayName = "Validate VatNumber format failed")]
        [InlineData("a11111")]
        [InlineData("A11111-1")]
        [InlineData("A1111 1")]
        public void Validate_VatNumber_format_failed(string input)
        {
            IInputInvoice entity = this.CreateEntity();
            entity.VatNumber = input;

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "El campo 'NIF' tiene formato incorrecto");
        }

        [Fact(DisplayName = "Validate VatNumber length failed")]
        public void Validate_VatNumber_length_failed()
        {
            IInputInvoice entity = this.CreateEntity();
            entity.VatNumber = "123456789012345678901";

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "El campo 'NIF' tiene longitud incorrecta");
        }

        [Fact(DisplayName = "Validate partnerName length failed")]
        public void Validate_partnerName_length_failed()
        {
            IInputInvoice entity = this.CreateEntity();
            entity.PartnerName = @"0123456789
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

            errors.Should().Contain(x => !x.IsValid && x.Code == "El campo 'Nombre de cliente' tiene longitud incorrecta");
        }


        [Fact(DisplayName = "Validate source required failed")]
        public void Validate_source_required_failed()
        {
            IInputInvoice entity = this.CreateEntity();
            entity.Source = "";

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "El campo 'Origen', obligatorio contenido");
        }

        [Theory(DisplayName = "Validate PostalCode format failed")]
        [InlineData("123456")]
        [InlineData("1234")]
        [InlineData("1234A")]
        public void Validate_PostalCode_format_failed(string input)
        {
            IInputInvoice entity = this.CreateEntity();
            entity.PostalCode = input;

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "El campo 'Código postal' tiene formato incorrecto");

        }

        [Fact(DisplayName = "Validate null postalCode succeed")]
        public void Validate_null_PostalCode_succeed()
        {
            IInputInvoice entity = this.CreateEntity();
            entity.PostalCode = null;

            var errors = this.validation.Validate(entity).ToList();

            errors.Count.Should().Be(0);
        }

        [Fact(DisplayName = "Validate null countryCode succeed")]
        public void Validate_null_countryCode_succeed()
        {
            IInputInvoice entity = this.CreateEntity();
            entity.CountryCode = null;

            var errors = this.validation.Validate(entity).ToList();

            errors.Count.Should().Be(0);
        }

        [Fact(DisplayName = "Validate empty countryCode succeed")]
        public void Validate_empty_countryCode_succeed()
        {
            IInputInvoice entity = this.CreateEntity();
            entity.CountryCode = String.Empty;

            var errors = this.validation.Validate(entity).ToList();

            errors.Count.Should().Be(0);
        }

        [Theory(DisplayName = "Validate CountryCode format failed")]
        [InlineData("000")]
        public void Validate_CountryCode_format_failed(string input)
        {
            IInputInvoice entity = this.CreateEntity();
            entity.CountryCode = input;

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "El campo 'Código país' tiene longitud incorrecta");

        }

        [Theory(DisplayName = "Validate vatType format failed"),
         InlineData(-1),
         InlineData(9)]
        public void Validate_VatType_format_failed(int input)
        {
            IInputInvoice entity = this.CreateEntity();
            entity.VatType = input;

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "El campo 'Tipo de documento' tiene formato incorrecto");

        }

        [Theory(DisplayName = "Validate pending and satisfied amounts failed")]
        [InlineData(null, 20.02)]
        [InlineData(10.01, null)]
        public void Validate_pending_and_satisfied_failed(double? pending, double? satisfied)
        {
            IInputInvoice entity = this.CreateEntity();
            entity.PendingAmount = (decimal?)pending;
            entity.SatisfiedAmount = (decimal?)satisfied;

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "El campo 'Solo un importe informado' tiene valor incorrecto");
        }

        [Theory(DisplayName = "Validate pending and satisfied amounts succeed")]
        [InlineData(10.01, 20.02)]
        [InlineData(null, null)]
        public void Validate_pending_and_satisfied_succeed(double? pending, double? satisfied)
        {
            IInputInvoice entity = this.CreateEntity();
            entity.PendingAmount = (decimal?)pending;
            entity.SatisfiedAmount = (decimal?)satisfied;

            var errors = this.validation.Validate(entity).ToList();

            errors.Count.Should().Be(0);
        }

        private IInputInvoice CreateEntity()
        {
            return new InputInvoice()
            {
                Id = Guid.NewGuid(),
                Line = 1,
                InvoiceDate = DateTime.Now,
                JournalDate = DateTime.Now,
                InvoiceNumber = "number",
                PartnerAccount = "43000001",
                PartnerName = "name",
                VatNumber = "53336699A",
                Lines = new List<IInputInvoiceLine>()
                {
                    this.CreateEntityLine()
                }.ToArray(),
                Source = "source"
            };
        }

        private IInputInvoiceLine CreateEntityLine()
        {
            return new InputInvoiceLine()
            {
                Id = Guid.NewGuid(),
                Line = 1,
                BaseAmount = 1210,
                TaxAmount = 210,
                Transaction = "OP_INT",
            };
        }
    }
}
