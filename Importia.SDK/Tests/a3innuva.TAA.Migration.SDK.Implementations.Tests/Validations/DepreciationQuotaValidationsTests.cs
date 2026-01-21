using System;
using System.Linq;
using FluentAssertions;
using Xunit;
using a3innuva.TAA.Migration.SDK.Interfaces;

namespace a3innuva.TAA.Migration.SDK.Implementations.Tests
{
    [Trait("Unit test", "DepreciationQuotaValidations")]
    public class DepreciationQuotaValidationsTests
    {
        private DepreciationQuotaValidation validation;

        public DepreciationQuotaValidationsTests()
        {
            this.validation = new DepreciationQuotaValidation();
        }

        ~DepreciationQuotaValidationsTests()
        {
            this.validation = null;
        }

        [Fact(DisplayName = "Validate succeed")]
        public void Validate_succeed()
        {
            var entity = this.CreateEntity();

            var errors = this.validation.Validate(entity).ToList();

            errors.Count.Should().Be(0);
        }

        [Fact(DisplayName = "Validate AccountingYear required failed")]
        public void Validate_AccountingYear_required_failed()
        {
            var entity = this.CreateEntity();
            entity.AccountingYear = 0;

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "El campo 'Ejercicio contable', obligatorio contenido");
        }

        [Fact(DisplayName = "Validate AccountingQuotaAmount required failed")]
        public void Validate_AccountingQuotaAmount_required_failed()
        {
            var entity = this.CreateEntity();
            entity.AccountingQuotaAmount = 0m;

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "El campo 'Cuota contable', obligatorio contenido");
        }

        [Fact(DisplayName = "Validate FiscalQuotaAmount required failed")]
        public void Validate_FiscalQuotaAmount_required_failed()
        {
            var entity = this.CreateEntity();
            entity.FiscalQuotaAmount = 0m;

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "El campo 'Cuota fiscal', obligatorio contenido");
        }

        [Fact(DisplayName = "Validate StartDate required failed")]
        public void Validate_StartDate_required_failed()
        {
            var entity = this.CreateEntity();
            entity.StartDate = new DateTime(2101, 1, 1);

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "El campo 'Inicio de periodo de amortización', obligatorio contenido");
        }

        [Fact(DisplayName = "Validate EndDate required failed")]
        public void Validate_EndDate_required_failed()
        {
            var entity = this.CreateEntity();
            entity.EndDate = new DateTime(2101, 1, 1);

            var errors = this.validation.Validate(entity);

            errors.Should().Contain(x => !x.IsValid && x.Code == "El campo 'Fin de periodo de amortización', obligatorio contenido");
        }

        private IFixedAssetDepreciationQuota CreateEntity()
        {
            return new DepreciationQuotaTest
            {
                Id = Guid.NewGuid(),
                Line = 1,
                AccountingYear = 2024,
                AccountingQuotaAmount = 1200.50m,
                FiscalQuotaAmount = 1100.00m,
                StartDate = DateTime.UtcNow.AddMonths(-6),
                EndDate = DateTime.UtcNow
            };
        }

        // Minimal test implementation to satisfy IFixedAssetDepreciationQuota for the unit tests
        private class DepreciationQuotaTest : IFixedAssetDepreciationQuota
        {
            public Guid Id { get; set; }
            public int Line { get; set; }
            public int AccountingYear { get; set; }
            public decimal AccountingQuotaAmount { get; set; }
            public decimal FiscalQuotaAmount { get; set; }
            public DateTime? StartDate { get; set; }
            public DateTime? EndDate { get; set; }
            public string Identity() => $"{Id}";
        }
    }
}