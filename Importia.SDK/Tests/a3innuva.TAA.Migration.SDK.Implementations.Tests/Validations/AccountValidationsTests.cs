namespace a3innuva.TAA.Migration.SDK.Implementations.Tests
{
    using AwesomeAssertions;
    using System;
    using System.Linq;
    using Xunit;

    [Trait("Unit test", "AccountValidations")]
    public class AccountValidationsTests
    {
        private AccountValidation validation;

        public AccountValidationsTests()
        {
            this.validation = new AccountValidation();
        }

        ~AccountValidationsTests()
        {
            this.validation = null;
        }

        [Fact(DisplayName = "Validate succeed")]
        public void Validate_succeed()
        {
            Account entity = this.CreateEntity();

            var errors = this.validation.Validate(entity).ToList();

            errors.Count.Should().Be(0);
        }

        [Theory(DisplayName = "Validate Code required failed")]
        [InlineData(null)]
        [InlineData(" ")]
        public void Validate_Code_required_failed(string code)
        {
            Account entity = this.CreateEntity();
            entity.Code = code;

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "El campo 'Cuenta', obligatorio contenido");
        }

        [Theory(DisplayName = "Validate Code numeric failed")]
        [InlineData("000000")]
        [InlineData("A00000")]
        public void Validate_Code_numeric_failed(string input)
        {
            Account entity = this.CreateEntity();
            entity.Code = input;

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "El campo 'Cuenta' tiene formato incorrecto");
        }

        [Fact(DisplayName = "Validate Code length failed")]
        public void Validate_Code_length_failed()
        {
            Account entity = this.CreateEntity();
            entity.Code = "123456789012345678901";

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "El campo 'Cuenta' tiene longitud incorrecta");
        }

        [Fact(DisplayName = "Validate Description required failed")]
        public void Validate_Description_required_failed()
        {
            Account entity = this.CreateEntity();
            entity.Description = "";

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "El campo 'Descripción', obligatorio contenido");
        }

        [Theory(DisplayName = "Validate Description alphanumeric succeed")]
        [InlineData("Proveedores (euros)")]
        [InlineData("DEUDAS A LARGO PLAZO POR PRÉS")]
        [InlineData("Cliente 430")]
        public void Validate_Description_alphanumeric_succeed(string input)
        {
            Account entity = this.CreateEntity();
            entity.Description = input;

            var errors = this.validation.Validate(entity);

            errors.Count().Should().Be(0);
        }

        [Fact(DisplayName = "Validate Description length failed")]
        public void Validate_Description_length_failed()
        {
            Account entity = this.CreateEntity();
            entity.Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce interdum gravida dolor, vehicula lobortis turpis tristique rhoncus. Donec sit amet fringilla sapien. Interdum et malesuada fames ac ante ipsum primis in faucibus. Nam sodales nisl id augue sed.";

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "El campo 'Descripción' tiene longitud incorrecta");
        }

        [Theory(DisplayName = "Validate Fiscal data complete failed")]
        [InlineData("X1010101", "", "")]
        [InlineData("", "Proveedores (euros)", "")]
        [InlineData("", "", "08901")]
        public void Validate_Fiscal_data_complete_failed(string vatNumber, string name, string postalCode)
        {
            Account entity = this.CreateEntity();
            entity.VatNumber = vatNumber;
            entity.Name = name;
            entity.PostalCode = postalCode;

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "Datos fiscales incompletos");
        }

        [Theory(DisplayName = "Validate level failed")]
        [InlineData("X1010101", "", "")]
        [InlineData("", "Proveedores (euros)", "")]
        [InlineData("", "", "08901")]
        public void Validate_level_failed(string vatNumber, string name, string postalCode)
        {
            Account entity = this.CreateEntity();
            entity.Code = "43001";
            entity.VatNumber = vatNumber;
            entity.Name = name;
            entity.PostalCode = postalCode;

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "Un nivel de plan contable no admite datos fiscales");
        }

        [Fact(DisplayName = "Validate level succeed")]
        public void Validate_level_succeed()
        {
            Account entity = this.CreateEntity();
            entity.Code = "43001";
            entity.VatNumber = null;
            entity.Name = null;
            entity.PostalCode = null;

            var errors = this.validation.Validate(entity).ToList();

            errors.Count.Should().Be(0);
        }

        [Theory(DisplayName = "Validate VatNumber format failed")]
        [InlineData("a11111")]
        [InlineData("A11111-1")]
        [InlineData("A1111 1")]
        public void Validate_VatNumber_format_failed(string input)
        {
            Account entity = this.CreateEntity();
            entity.VatNumber = input;

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "El campo 'NIF' tiene formato incorrecto");
        }

        [Fact(DisplayName = "Validate VatNumber length failed")]
        public void Validate_VatNumber_length_failed()
        {
            Account entity = this.CreateEntity();
            entity.VatNumber = "123456789012345678901";

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "El campo 'NIF' tiene longitud incorrecta");
        }


        [Theory(DisplayName = "Validate Name format succeed")]
        [InlineData("Nombre fiscal")]
        [InlineData("Cliente 430")]
        [InlineData("Pachéco y cia")]
        public void Validate_Name_format_succeed(string input)
        {
            Account entity = this.CreateEntity();
            entity.Name = input;

            var errors = this.validation.Validate(entity).ToList();

            errors.Count.Should().Be(0);
        }

        [Fact(DisplayName = "Validate Name length failed")]
        public void Validate_Name_length_failed()
        {
            Account entity = this.CreateEntity();
            entity.Name = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce interdum gravida dolor, vehicula lobortis turpis tristique rhoncus. Donec sit amet fringilla sapien. Interdum et malesuada fames ac ante ipsum primis in faucibus. Nam sodales nisl id augue sed.";

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "El campo 'Nombre fiscal' tiene longitud incorrecta");
        }

        [Theory(DisplayName = "Validate PostalCode format failed")]
        [InlineData("123456")]
        [InlineData("1234")]
        [InlineData("1234A")]
        public void Validate_PostalCode_format_failed(string input)
        {
            Account entity = this.CreateEntity();
            entity.PostalCode = input;

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "El campo 'Código postal' tiene formato incorrecto");

        }

        [Fact(DisplayName = "Validate null postalCode succeed")]
        public void Validate_null_PostalCode_succeed()
        {
            Account entity = this.CreateEntity();
            entity.PostalCode = null;

            var errors = this.validation.Validate(entity).ToList();

            errors.Count.Should().Be(0);
        }


        [Fact(DisplayName = "Validate empty Id failed")]
        public void Validate_empty_id_failed()
        {
            Account entity = this.CreateEntity();
            entity.Id = Guid.Empty;

            var errors = this.validation.Validate(entity).ToList();

            errors.Count.Should().Be(1);
        }

        [Fact(DisplayName = "Validate null countryCode succeed")]
        public void Validate_null_countryCode_succeed()
        {
            Account entity = this.CreateEntity();
            entity.CountryCode = null;

            var errors = this.validation.Validate(entity).ToList();

            errors.Count.Should().Be(0);
        }

        [Fact(DisplayName = "Validate empty countryCode succeed")]
        public void Validate_empty_countryCode_succeed()
        {
            Account entity = this.CreateEntity();
            entity.CountryCode = String.Empty;

            var errors = this.validation.Validate(entity).ToList();

            errors.Count.Should().Be(0);
        }

        [Theory(DisplayName = "Validate CountryCode format failed")]
        [InlineData("000")]
        public void Validate_CountryCode_format_failed(string input)
        {
            Account entity = this.CreateEntity();
            entity.CountryCode = input;

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "El campo 'Código país' tiene longitud incorrecta");

        }

        [Theory(DisplayName = "Validate vatType format failed"),
        InlineData(-1),
        InlineData(9)]
        public void Validate_VatType_format_failed(int input)
        {
            Account entity = this.CreateEntity();
            entity.VatType = input;

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "El campo 'Tipo de documento' tiene formato incorrecto");

        }

        [Theory(DisplayName = "Validate CountryCode uppercase two letters failed")]
        [InlineData("E")]
        [InlineData("es")]
        [InlineData("Es")]
        [InlineData("E1")]
        [InlineData("12")]
        [InlineData(" ")]
        public void Validate_CountryCode_uppercase_failed(string input)
        {
            Account entity = this.CreateEntity();
            entity.CountryCode = input;

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "El campo 'Código país' tiene formato incorrecto");
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
            Account entity = this.CreateEntity();
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
            Account entity = this.CreateEntity();
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
            Account entity = this.CreateEntity();
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
            Account entity = this.CreateEntity();
            entity.VatNumber = vatNumber;
            entity.CountryCode = "ES";

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "Los dos primeros caracteres del NIF intracomunitario deben coincidir con el país");
        }

        [Theory(DisplayName = "Validate vat number without country succeed")]
        [InlineData("11111111H")]
        [InlineData("FR12345678901")]
        public void Validate_vat_number_without_country_succeed(string vatNumber)
        {
            Account entity = this.CreateEntity();
            entity.VatNumber = vatNumber;
            entity.CountryCode = null;

            var errors = this.validation.Validate(entity).ToList();

            errors.Count.Should().Be(0);
        }

        private Account CreateEntity()
        {
            return new Account()
            {
                Id = Guid.NewGuid(),
                Line = 1,
                Code = "43001101",
                Description = "cuenta cliente",
                Name = "Nombre",
                PostalCode = "08000",
                VatNumber = "A123456789",
                VatType = 0,
                CountryCode = "ES"
            };
        }
    }
}
