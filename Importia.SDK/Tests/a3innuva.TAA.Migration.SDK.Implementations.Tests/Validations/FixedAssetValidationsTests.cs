using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using Xunit;
using a3innuva.TAA.Migration.SDK.Interfaces;

namespace a3innuva.TAA.Migration.SDK.Implementations.Tests
{
    [Trait("Unit test", "FixedAssetValidations")]
    public class FixedAssetValidationsTests
    {
        private FixedAssetValidation validation;

        public FixedAssetValidationsTests()
        {
            this.validation = new FixedAssetValidation();
        }

        ~FixedAssetValidationsTests()
        {
            this.validation = null;
        }

        [Fact(DisplayName = "Validate succeed for full fixed asset")]
        public void Validate_succeed_for_full_fixedasset()
        {
            var entity = this.CreateEntity();

            var errors = this.validation.Validate(entity).ToList();

            // For a properly populated entity we expect no validation errors
            errors.Count.Should().Be(0);
        }

        [Fact(DisplayName = "Validate fails when depreciation quotas invalid")]
        public void Validate_fails_when_depreciation_quotas_invalid()
        {
            var entity = this.CreateEntity();
            // replace quotas with an invalid quota
            entity.DepreciationQuotas = new IFixedAssetDepreciationQuota[]
            {
                new DepreciationQuotaTest
                {
                    Id = Guid.NewGuid(),
                    Line = 1,
                    AccountingYear = 0, // invalid
                    AccountingQuotaAmount = 0m,
                    FiscalQuotaAmount = 0m,
                    StartDate = null,
                    EndDate = null
                }
            };

            var errors = this.validation.Validate(entity).ToList();

            // We expect at least one validation error when quotas are invalid
            errors.Count.Should().BeGreaterThan(0);
            errors.Should().Contain(x => !x.IsValid);
        }

        [Fact(DisplayName = "Validate fails with retirement invoice number and no sale reason")]
        public void Validate_fails_with_retirement_invoice_number_and_no_sale_reason()
        {
            var entity = this.CreateEntity();

            entity.RetirementReason = null; // No sale reason
            entity.RetirementInvoiceNumber = "123";

            var errors = this.validation.Validate(entity).ToList();

            // We expect at least one validation error when quotas are invalid
            errors.Count.Should().BeGreaterThan(0);
            errors.Should().Contain(x => !x.IsValid);
        }

        [Fact(DisplayName = "Validate fails with retirement disposal value and no sale reason")]
        public void Validate_fails_with_retirement_disposal_value_and_no_sale_reason()
        {
            var entity = this.CreateEntity();

            entity.RetirementReason = null; // No sale reason
            entity.RetirementDisposalValue = 1000m;

            var errors = this.validation.Validate(entity).ToList();

            // We expect at least one validation error when quotas are invalid
            errors.Count.Should().BeGreaterThan(0);
            errors.Should().Contain(x => !x.IsValid);
        }

        [Fact(DisplayName = "Validate fails with retirement base amount and no sale reason")]
        public void Validate_fails_with_retirement_base_amount_and_no_sale_reason()
        {
            var entity = this.CreateEntity();

            entity.RetirementReason = null; // No sale reason
            entity.RetirementBaseAmount = 500m;

            var errors = this.validation.Validate(entity).ToList();

            // We expect at least one validation error when quotas are invalid
            errors.Count.Should().BeGreaterThan(0);
            errors.Should().Contain(x => !x.IsValid);
        }

        [Fact(DisplayName = "Validate fails with retirement tax code and no sale reason")]
        public void Validate_fails_with_retirement_tax_code_and_no_sale_reason()
        {
            var entity = this.CreateEntity();

            entity.RetirementReason = null; // No sale reason
            entity.RetirementTaxCode = "TC01";

            var errors = this.validation.Validate(entity).ToList();

            // We expect at least one validation error when quotas are invalid
            errors.Count.Should().BeGreaterThan(0);
            errors.Should().Contain(x => !x.IsValid);
        }

        [Fact(DisplayName = "Validate fails with retirement tax amount and no sale reason")]
        public void Validate_fails_with_retirement_tax_amount_and_no_sale_reason()
        {
            var entity = this.CreateEntity();

            entity.RetirementReason = null; // No sale reason
            entity.RetirementTaxAmount = 100m;

            var errors = this.validation.Validate(entity).ToList();

            // We expect at least one validation error when quotas are invalid
            errors.Count.Should().BeGreaterThan(0);
            errors.Should().Contain(x => !x.IsValid);
        }

        [Fact(DisplayName = "Validate fails with retirement is exempt and no sale reason")]
        public void Validate_fails_with_retirement_is_exempt_and_no_sale_reason()
        {
            var entity = this.CreateEntity();

            entity.RetirementReason = null; // No sale reason
            entity.RetirementIsExempt = true;

            var errors = this.validation.Validate(entity).ToList();

            // We expect at least one validation error when quotas are invalid
            errors.Count.Should().BeGreaterThan(0);
            errors.Should().Contain(x => !x.IsValid);
        }

        private IFixedAsset CreateEntity()
        {
            return new FixedAssetTest
            {
                Id = Guid.NewGuid(),
                Line = 1,
                Source = "extern",
                AccountCode = "600000",
                AccountDescription = "Cuenta activo",
                TypeOfGood = 1,
                Identifier = "FA-001",
                Description = "Equipo informático",
                IsCapitalAsset = false,
                AccumulatedDepreciationAccountCode = "680000",
                AccumulatedDepreciationAccountDescription = "Amortización acumulada",
                EndowmentAccountCode = "781000",
                EndowmentAccountDescription = "Dotación",
                AcquisitionDate = DateTime.UtcNow.AddYears(-1),
                AcquisitionValue = 5000m,
                AcquisitionInvoiceNumber = "INV-123",
                AcquisitionPartnerName = "Proveedor S.L.",
                AcquisitionVatNumber = "B12345678",
                AcquisitionPartnerAccount = "430000",
                AcquisitionPostalCode = "28001",
                AcquisitionVatType = 1,
                AcquisitionCountryCode = "ES",
                AcquisitionBaseAmount = 4200m,
                AcquisitionTaxCode = "TC01",
                AcquisitionTaxAmount = 800m,
                AcquisitionProrateApply = false,
                AcquisitionProrateAmount = null,
                AcquisitionDeductibleAmount = null,
                AssetEndDate = null,
                RetirementReason = 2,
                AssetStartDate = DateTime.UtcNow.AddMonths(-11),
                DepreciationResidualValue = 200m,
                DepreciationYears = 5,
                DepreciationFiscalYears = 5,
                DepreciationQuotas = new IFixedAssetDepreciationQuota[]
                {
                    new DepreciationQuotaTest
                    {
                        Id = Guid.NewGuid(),
                        Line = 1,
                        AccountingYear = 2024,
                        AccountingQuotaAmount = 1000m,
                        FiscalQuotaAmount = 900m,
                        StartDate = DateTime.UtcNow.AddMonths(-11),
                        EndDate = DateTime.UtcNow
                    }
                }
            };
        }

        // Minimal IFixedAsset implementation for tests
        private class FixedAssetTest : IFixedAsset
        {
            public Guid Id { get; set; }
            public int Line { get; set; }

            public string Source { get; set; }

            public string AccountCode { get; set; }
            public string AccountDescription { get; set; }
            public int TypeOfGood { get; set; }
            public string Identifier { get; set; }
            public string Description { get; set; }
            public bool IsCapitalAsset { get; set; }
            public string AccumulatedDepreciationAccountCode { get; set; }
            public string AccumulatedDepreciationAccountDescription { get; set; }
            public string EndowmentAccountCode { get; set; }
            public string EndowmentAccountDescription { get; set; }
            public DateTime AcquisitionDate { get; set; }
            public decimal AcquisitionValue { get; set; }
            public string AcquisitionInvoiceNumber { get; set; }
            public string AcquisitionPartnerName { get; set; }
            public string AcquisitionVatNumber { get; set; }
            public string AcquisitionPartnerAccount { get; set; }
            public string AcquisitionPostalCode { get; set; }
            public int AcquisitionVatType { get; set; }
            public string AcquisitionCountryCode { get; set; }
            public decimal? AcquisitionBaseAmount { get; set; }
            public string AcquisitionTaxCode { get; set; }
            public decimal? AcquisitionTaxAmount { get; set; }
            public bool AcquisitionProrateApply { get; set; }
            public decimal? AcquisitionProrateAmount { get; set; }
            public decimal? AcquisitionDeductibleAmount { get; set; }
            public DateTime? AssetEndDate { get; set; }
            public int? RetirementReason { get; set; }
            public string RetirementInvoiceNumber { get; set; }
            public decimal? RetirementDisposalValue { get; set; }
            public decimal? RetirementBaseAmount { get; set; }
            public string RetirementTaxCode { get; set; }
            public decimal? RetirementTaxAmount { get; set; }
            public bool RetirementIsExempt { get; set; }
            public DateTime AssetStartDate { get; set; }
            public decimal? DepreciationResidualValue { get; set; }
            public decimal DepreciationYears { get; set; }
            public decimal DepreciationFiscalYears { get; set; }
            public IEnumerable<IFixedAssetDepreciationQuota> DepreciationQuotas { get; set; }

            public string Identity() => $"{Id}";
        }

        // Minimal IFixedAssetDepreciationQuota implementation used in tests
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