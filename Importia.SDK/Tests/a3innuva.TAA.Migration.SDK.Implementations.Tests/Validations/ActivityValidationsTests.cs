using a3innuva.TAA.Migration.SDK.Interfaces;

namespace a3innuva.TAA.Migration.SDK.Implementations.Tests
{
    using FluentAssertions;
    using System;
    using System.Linq;
    using Xunit;

    [Trait("Unit test", "ActivityValidations")]
    public class ActivityValidationsTests
    {
        private ActivityValidation validation;

        public ActivityValidationsTests()
        {
            this.validation = new ActivityValidation();
        }

        ~ActivityValidationsTests()
        {
            this.validation = null;
        }

        [Theory(DisplayName = "Validate succeed")]
        [InlineData(Taxation.State)]
        [InlineData(Taxation.CanaryIsland)]
        public void Validate_succeed(Taxation taxation)
        {
            Activity entity = this.CreateEntity();
            entity.Taxation = taxation;

            var errors = this.validation.Validate(entity).ToList();

            errors.Count.Should().Be(0);
        }

        [Fact(DisplayName = "Validate Id required failed")]
        public void Validate_Id_required_failed()
        {
            Activity entity = this.CreateEntity();
            entity.Id = Guid.Empty;

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "El campo 'Id', obligatorio contenido");
        }

        [Fact(DisplayName = "Validate Description required failed")]
        public void Validate_Description_required_failed()
        {
            Activity entity = this.CreateEntity();
            entity.Description = "";

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "El campo 'Descripción', obligatorio contenido");
        }
        
        [Fact(DisplayName = "Validate ShortDescription required failed")]
        public void Validate_ShortDescription_required_failed()
        {
            Activity entity = this.CreateEntity();
            entity.ShortDescription = "";

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "El campo 'Descripción corta', obligatorio contenido");
        }
        
        [Fact(DisplayName = "Validate Taxation required failed")]
        public void Validate_Taxationr_failed()
        {
            Activity entity = this.CreateEntity();
            entity.Taxation = (Taxation)99;

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "El campo 'Tributación' tiene formato incorrecto");
        }
        
        
        [Fact(DisplayName = "Validate Estimation Year failed")]
        public void Validate_Estimation_Yeare_failed()
        {
            Activity entity = this.CreateEntity();
            entity.Estimations[0].Year = 0;

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => CheckInvalidEstimationError(x));
        }

        [Fact(DisplayName = "Validate Estimation Epigraph failed")]
        public void Validate_Estimation_Epigraph_failed()
        {
            Activity entity = this.CreateEntity();
            entity.Estimations[0].Epigraph = 0;

            var errors = this.validation.Validate(entity);
            
            errors.Should().Contain(x => CheckInvalidEstimationError(x));
        }
        
        private bool CheckInvalidEstimationError(IValidationResult x)
        {
            return !x.IsValid && x.Code == "'Epígrafes inválidos'";
        }
        
        private Activity CreateEntity()
        {
            return new Activity()
            {
                Id = Guid.NewGuid(),
                Line = 1,
                Description = "Descripción larga",
                ShortDescription = "Corta",
                Taxation = Taxation.State,
                Source = "extern",
                Estimations = this.CreateEstimations()
            };
        }

        private IEstimation[] CreateEstimations()
        {
            return new IEstimation[]
            {
                new Estimation()
                {
                    Year = 2020,
                    Epigraph = 500
                }
            };
        }
    }
}
