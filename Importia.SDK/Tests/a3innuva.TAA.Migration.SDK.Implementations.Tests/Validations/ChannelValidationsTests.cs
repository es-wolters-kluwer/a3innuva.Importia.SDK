using a3innuva.TAA.Migration.SDK.Interfaces;

namespace a3innuva.TAA.Migration.SDK.Implementations.Tests
{
    using FluentAssertions;
    using System;
    using System.Linq;
    using Xunit;

    [Trait("Unit test", "ChannelValidations")]
    public class ChannelValidationsTests
    {
        private ChannelValidation validation;

        public ChannelValidationsTests()
        {
            this.validation = new ChannelValidation();
        }

        ~ChannelValidationsTests()
        {
            this.validation = null;
        }

        [Fact(DisplayName = "Validate succeed")]
        public void Validate_succeed()
        {
            Channel entity = this.CreateEntity();

            var errors = this.validation.Validate(entity).ToList();

            errors.Count.Should().Be(0);
        }

        [Fact(DisplayName = "Validate Id required failed")]
        public void Validate_Id_required_failed()
        {
            Channel entity = this.CreateEntity();
            entity.Id = Guid.Empty;

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "El campo 'Id', obligatorio contenido");
        }
        
        [Fact(DisplayName = "Validate Description required failed")]
        public void Validate_Description_required_failed()
        {
            Channel entity = this.CreateEntity();
            entity.Description = "";

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "El campo 'Descripción', obligatorio contenido");
        }
        
        [Fact(DisplayName = "Validate ShortDescription required failed")]
        public void Validate_ShortDescription_required_failed()
        {
            Channel entity = this.CreateEntity();
            entity.ShortDescription = "";

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "El campo 'Descripción corta', obligatorio contenido");
        }
        
        private Channel CreateEntity()
        {
            return new Channel()
            {
                Id = Guid.NewGuid(),
                Line = 1,
                Description = "Descripción larga canal",
                ShortDescription = "Corta canal",
                Source = "extern",
            };
        }
    }
}
