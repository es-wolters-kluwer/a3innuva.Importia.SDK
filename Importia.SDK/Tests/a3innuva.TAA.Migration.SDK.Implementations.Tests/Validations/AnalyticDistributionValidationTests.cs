namespace a3innuva.TAA.Migration.SDK.Implementations.Tests
{
    using FluentAssertions;
    using System.Collections.Generic;
    using a3innuva.TAA.Migration.SDK.Interfaces.Entities.Journal;
    using Xunit;

    [Trait("Unit test", "AnalyticDistributionValidation")]
    public class AnalyticDistributionValidationTests
    {
        private readonly AnalyticDistributionValidation validation;

        public AnalyticDistributionValidationTests()
        {
            this.validation = new AnalyticDistributionValidation();
        }

        [Fact(DisplayName = "Validate succeed")]
        public void Validate_succeed()
        {
            IAnalyticDistribution entity = this.CreateEntity();

            var errors = this.validation.Validate(entity, 1);

            errors.Should().BeEmpty();
        }

        [Fact(DisplayName = "Validate zero percentage failed")]
        public void Validate_zero_percentage_failed()
        {
            IAnalyticDistribution entity = this.CreateEntity();
            entity.Percentage = 0;

            var errors = this.validation.Validate(entity, 1);

            errors.Should().Contain(x => !x.IsValid && x.Code == "El campo 'Porcentaje de distribuci�n anal�tica' tiene valor incorrecto");
        }

        [Fact(DisplayName = "Validate null details failed")]
        public void Validate_null_details_failed()
        {
            IAnalyticDistribution entity = this.CreateEntity();
            entity.Details = null;

            var errors = this.validation.Validate(entity, 1);

            errors.Should().Contain(x => !x.IsValid && x.Code == "Cada distribuci�n anal�tica debe tener al menos un detalle");
        }

        [Fact(DisplayName = "Validate empty details failed")]
        public void Validate_empty_details_failed()
        {
            IAnalyticDistribution entity = this.CreateEntity();
            entity.Details = new List<IAnalyticDistributionDetail>();

            var errors = this.validation.Validate(entity, 1);

            errors.Should().Contain(x => !x.IsValid && x.Code == "Cada distribuci�n anal�tica debe tener al menos un detalle");
        }

        [Fact(DisplayName = "Validate detail empty level failed")]
        public void Validate_detail_empty_level_failed()
        {
            IAnalyticDistribution entity = this.CreateEntity();
            entity.Details = new List<IAnalyticDistributionDetail>
            {
                new AnalyticDistributionDetail { Level = "", CostCenter = "Madrid" }
            };

            var errors = this.validation.Validate(entity, 1);

            errors.Should().Contain(x => !x.IsValid && x.Code == "Los campos 'Level' y 'CostCenter' de los detalles de distribuci�n anal�tica son obligatorios");
        }

        [Fact(DisplayName = "Validate detail null level failed")]
        public void Validate_detail_null_level_failed()
        {
            IAnalyticDistribution entity = this.CreateEntity();
            entity.Details = new List<IAnalyticDistributionDetail>
            {
                new AnalyticDistributionDetail { Level = null, CostCenter = "Madrid" }
            };

            var errors = this.validation.Validate(entity, 1);

            errors.Should().Contain(x => !x.IsValid && x.Code == "Los campos 'Level' y 'CostCenter' de los detalles de distribuci�n anal�tica son obligatorios");
        }

        [Fact(DisplayName = "Validate detail empty cost center failed")]
        public void Validate_detail_empty_cost_center_failed()
        {
            IAnalyticDistribution entity = this.CreateEntity();
            entity.Details = new List<IAnalyticDistributionDetail>
            {
                new AnalyticDistributionDetail { Level = "Proyectos", CostCenter = "" }
            };

            var errors = this.validation.Validate(entity, 1);

            errors.Should().Contain(x => !x.IsValid && x.Code == "Los campos 'Level' y 'CostCenter' de los detalles de distribuci�n anal�tica son obligatorios");
        }

        [Fact(DisplayName = "Validate detail null cost center failed")]
        public void Validate_detail_null_cost_center_failed()
        {
            IAnalyticDistribution entity = this.CreateEntity();
            entity.Details = new List<IAnalyticDistributionDetail>
            {
                new AnalyticDistributionDetail { Level = "Proyectos", CostCenter = null }
            };

            var errors = this.validation.Validate(entity, 1);

            errors.Should().Contain(x => !x.IsValid && x.Code == "Los campos 'Level' y 'CostCenter' de los detalles de distribuci�n anal�tica son obligatorios");
        }

        [Fact(DisplayName = "Validate line number is propagated")]
        public void Validate_line_number_is_propagated()
        {
            IAnalyticDistribution entity = this.CreateEntity();
            entity.Percentage = 0;

            var errors = this.validation.Validate(entity, 5);

            errors.Should().Contain(x => !x.IsValid && x.Line == 5);
        }

        private IAnalyticDistribution CreateEntity()
        {
            return new AnalyticDistribution
            {
                Percentage = 60.35m,
                Details = new List<IAnalyticDistributionDetail>
                {
                    new AnalyticDistributionDetail { Level = "Proyectos", CostCenter = "Feria" },
                    new AnalyticDistributionDetail { Level = "Ciudades", CostCenter = "Madrid" }
                }
            };
        }
    }
}
