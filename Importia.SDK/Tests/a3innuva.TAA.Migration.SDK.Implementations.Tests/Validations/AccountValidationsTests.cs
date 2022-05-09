namespace a3innuva.TAA.Migration.SDK.Implementations.Tests
{
    using FluentAssertions;
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
