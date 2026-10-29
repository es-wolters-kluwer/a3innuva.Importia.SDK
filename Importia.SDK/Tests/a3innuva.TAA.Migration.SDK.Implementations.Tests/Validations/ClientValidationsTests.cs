namespace a3innuva.TAA.Migration.SDK.Implementations.Tests
{
    using AwesomeAssertions;
    using System;
    using System.Linq;
    using a3innuva.TAA.Migration.SDK.Interfaces;
    using Xunit;

    [Trait("Unit test", "ClientValidations")]
    public class ClientValidationsTests
    {
        private ClientValidation validation;

        public ClientValidationsTests()
        {
            this.validation = new ClientValidation();
        }

        ~ClientValidationsTests()
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

        [Theory(DisplayName = "Validate country code succeed")]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("ES")]
        [InlineData("FR")]
        public void Validate_country_code_succeed(string input)
        {
            IPartner entity = this.CreateEntity();
            entity.CountryCode = input;

            var errors = this.validation.Validate(entity).ToList();

            errors.Count.Should().Be(0);
        }

        [Theory(DisplayName = "Validate country code failed")]
        [InlineData("E")]
        [InlineData("ESP")]
        [InlineData("es")]
        [InlineData("Es")]
        [InlineData("E1")]
        [InlineData("12")]
        [InlineData(" ")]
        public void Validate_country_code_failed(string input)
        {
            IPartner entity = this.CreateEntity();
            entity.CountryCode = input;

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "El campo 'País' tiene formato incorrecto");
        }

        [Theory(DisplayName = "Validate spanish document with country succeed")]
        [InlineData("12345678Z", "ES")]
        [InlineData("X1234567L", "ES")]
        [InlineData("B12345674", "ES")]
        [InlineData("B1234567J", "ES")]
        [InlineData("12345678Z", null)]
        [InlineData("FR12345678901", "FR")]
        [InlineData("ESB12345674", "ES")]
        [InlineData("FR12345678901", null)]
        public void Validate_vat_number_country_succeed(string vatNumber, string country)
        {
            IPartner entity = this.CreateEntity();
            entity.VatNumber = vatNumber;
            entity.CountryCode = country;

            var errors = this.validation.Validate(entity).ToList();

            errors.Count.Should().Be(0);
        }

        [Theory(DisplayName = "Validate spanish document with other country failed")]
        [InlineData("12345678Z", "FR")]
        [InlineData("X1234567L", "PT")]
        [InlineData("B12345674", "DE")]
        public void Validate_spanish_document_country_failed(string vatNumber, string country)
        {
            IPartner entity = this.CreateEntity();
            entity.VatNumber = vatNumber;
            entity.CountryCode = country;

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "Para un NIF, CIF o NIE el único país válido es 'ES'");
        }

        [Theory(DisplayName = "Validate intra community vat number country failed")]
        [InlineData("FR12345678901", "DE")]
        [InlineData("PT123456789", "ES")]
        public void Validate_intra_community_country_failed(string vatNumber, string country)
        {
            IPartner entity = this.CreateEntity();
            entity.VatNumber = vatNumber;
            entity.CountryCode = country;

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "Los dos primeros caracteres del NIF intracomunitario deben coincidir con el país");
        }

        [Theory(DisplayName = "Validate spanish document with ES country succeed")]
        [InlineData("A58417346")]
        [InlineData("B61441267")]
        [InlineData("C43123520")]
        [InlineData("E79823134")]
        [InlineData("F43548049")]
        [InlineData("G08480378")]
        [InlineData("H59008441")]
        [InlineData("P0700005B")]
        [InlineData("S0811001G")]
        [InlineData("Q0800007G")]
        [InlineData("Q5850014A")]
        [InlineData("S2800556I")]
        [InlineData("P1304700F")]
        [InlineData("P8890001D")]
        [InlineData("R2801324A")]
        [InlineData("K2345670S")]
        [InlineData("X8541111S")]
        [InlineData("Y0018909V")]
        [InlineData("11111111H")]
        [InlineData("45987452X")]
        [InlineData("32569874B")]
        public void Validate_spanish_document_es_country_succeed(string vatNumber)
        {
            IPartner entity = this.CreateEntity();
            entity.VatNumber = vatNumber;
            entity.CountryCode = "ES";

            var errors = this.validation.Validate(entity).ToList();

            errors.Count.Should().Be(0);
        }

        [Theory(DisplayName = "Validate spanish document with foreign country failed")]
        [InlineData("A58417346")]
        [InlineData("B61441267")]
        [InlineData("C43123520")]
        [InlineData("E79823134")]
        [InlineData("F43548049")]
        [InlineData("G08480378")]
        [InlineData("H59008441")]
        [InlineData("P0700005B")]
        [InlineData("S0811001G")]
        [InlineData("Q0800007G")]
        [InlineData("Q5850014A")]
        [InlineData("S2800556I")]
        [InlineData("P1304700F")]
        [InlineData("P8890001D")]
        [InlineData("R2801324A")]
        [InlineData("K2345670S")]
        [InlineData("X8541111S")]
        [InlineData("Y0018909V")]
        [InlineData("11111111H")]
        [InlineData("45987452X")]
        [InlineData("32569874B")]
        public void Validate_spanish_document_foreign_country_failed(string vatNumber)
        {
            IPartner entity = this.CreateEntity();
            entity.VatNumber = vatNumber;
            entity.CountryCode = "FR";

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "Para un NIF, CIF o NIE el único país válido es 'ES'");
        }

        [Theory(DisplayName = "Validate intra community vat number with matching country succeed")]
        [InlineData("AT123456789")]
        [InlineData("BE123456789")]
        [InlineData("BE1234567890")]
        [InlineData("BG123456789")]
        [InlineData("BG1234567890")]
        [InlineData("CY123456789")]
        [InlineData("CZ12345678")]
        [InlineData("CZ123456789")]
        [InlineData("CZ1234567890")]
        [InlineData("DE123456789")]
        [InlineData("DK12345678")]
        [InlineData("EE123456789")]
        [InlineData("EL123456789")]
        [InlineData("FI12345678")]
        [InlineData("FR12345678901")]
        [InlineData("GB12345")]
        [InlineData("GB123456789")]
        [InlineData("GB123456789012")]
        [InlineData("HR12345678901")]
        [InlineData("XI12345")]
        [InlineData("XI123456789")]
        [InlineData("XI123456789012")]
        [InlineData("HU12345678")]
        [InlineData("IE12345678")]
        [InlineData("IE123456789")]
        [InlineData("IT12345678901")]
        [InlineData("LT123456789")]
        [InlineData("LT123456789012")]
        [InlineData("LU12345678")]
        [InlineData("LV12345678901")]
        [InlineData("MT12345678")]
        [InlineData("NL123456789012")]
        [InlineData("PL1234567890")]
        [InlineData("PT123456789")]
        [InlineData("RO12")]
        [InlineData("RO123")]
        [InlineData("RO12345")]
        [InlineData("RO123456789")]
        [InlineData("RO1234567890")]
        [InlineData("SE123456789012")]
        [InlineData("SI12345678")]
        [InlineData("SK1234567890")]
        public void Validate_intra_community_matching_country_succeed(string vatNumber)
        {
            IPartner entity = this.CreateEntity();
            entity.VatNumber = vatNumber;
            entity.CountryCode = vatNumber.Substring(0, 2);

            var errors = this.validation.Validate(entity).ToList();

            errors.Count.Should().Be(0);
        }

        [Theory(DisplayName = "Validate intra community vat number with different country failed")]
        [InlineData("AT123456789")]
        [InlineData("BE123456789")]
        [InlineData("BE1234567890")]
        [InlineData("BG123456789")]
        [InlineData("BG1234567890")]
        [InlineData("CY123456789")]
        [InlineData("CZ12345678")]
        [InlineData("CZ123456789")]
        [InlineData("CZ1234567890")]
        [InlineData("DE123456789")]
        [InlineData("DK12345678")]
        [InlineData("EE123456789")]
        [InlineData("EL123456789")]
        [InlineData("FI12345678")]
        [InlineData("FR12345678901")]
        [InlineData("GB12345")]
        [InlineData("GB123456789")]
        [InlineData("GB123456789012")]
        [InlineData("HR12345678901")]
        [InlineData("XI12345")]
        [InlineData("XI123456789")]
        [InlineData("XI123456789012")]
        [InlineData("HU12345678")]
        [InlineData("IE12345678")]
        [InlineData("IE123456789")]
        [InlineData("IT12345678901")]
        [InlineData("LT123456789")]
        [InlineData("LT123456789012")]
        [InlineData("LU12345678")]
        [InlineData("LV12345678901")]
        [InlineData("MT12345678")]
        [InlineData("NL123456789012")]
        [InlineData("PL1234567890")]
        [InlineData("PT123456789")]
        [InlineData("RO12")]
        [InlineData("RO123")]
        [InlineData("RO12345")]
        [InlineData("RO123456789")]
        [InlineData("RO1234567890")]
        [InlineData("SE123456789012")]
        [InlineData("SI12345678")]
        [InlineData("SK1234567890")]
        public void Validate_intra_community_different_country_failed(string vatNumber)
        {
            IPartner entity = this.CreateEntity();
            entity.VatNumber = vatNumber;
            entity.CountryCode = "ES";

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "Los dos primeros caracteres del NIF intracomunitario deben coincidir con el país");
        }

        private IPartner CreateEntity()
        {
            return new Partner()
            {
                Id = Guid.NewGuid(),
                MaturitiesAccountCode = "43001101",
                TransactionCode = "OP_INT",
                CounterPartAccountCode = "77000000",
                Name = "My company",
                MaturitiesPeriodicity = new[] { 2, 0, 0, 0, 0, 0 },
                Taxation = Taxation.State
            };
        }
    }
}
