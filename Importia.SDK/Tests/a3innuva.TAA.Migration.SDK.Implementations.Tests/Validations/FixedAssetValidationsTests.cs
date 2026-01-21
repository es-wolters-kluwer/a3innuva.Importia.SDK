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

        private IFixedAsset CreateEntity()
        {
            return new FixedAssetTest
            {
                Id = Guid.NewGuid(),
                Line = 1,
                Source = "extern",
                IdentificationAccountCode = "600000",
                IdentificationAccountDescription = "Cuenta activo",
                IdentificationTypeOfGood = 1,
                IdentificationIdentifier = "FA-001",
                IdentificationDescription = "Equipo informático",
                IdentificationIsCapitalAsset = false,
                IdentificationAccumulatedDepreciationAccountCode = "680000",
                IdentificationAccumulatedDepreciationAccountDescription = "Amortización acumulada",
                IdentificationEndowmentAccountCode = "781000",
                IdentificationEndowmentAccountDescription = "Dotación",
                IdentificationAcquisitionDate = DateTime.UtcNow.AddYears(-1),
                IdentificationAcquisitionValue = 5000m,
                IdentificationInvoiceNumber = "INV-123",
                IdentificationPartnerName = "Proveedor S.L.",
                IdentificationVatNumber = "B12345678",
                IdentificationPartnerAccount = "430000",
                IdentificationPostalCode = "28001",
                IdentificationVatType = 1,
                IdentificationCountryCode = "ES",
                IdentificationBaseAmount = 4200m,
                IdentificationTaxCode = "TC01",
                IdentificationTaxAmount = 800m,
                IdentificationProrateApply = false,
                IdentificationProrateAmountValue = null,
                IdentificationDeductibleAmountValue = null,
                IdentificationAssetEndDate = null,
                IdentificationRetirementReason = null,
                RepaymentDataAssetStartDate = DateTime.UtcNow.AddMonths(-11),
                RepaymentDataResidualValue = 200m,
                RepaymentDataDepreciationYears = 5,
                RepaymentDataFiscalDepreciationYears = 5,
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

            public string IdentificationAccountCode { get; set; }
            public string IdentificationAccountDescription { get; set; }
            public int IdentificationTypeOfGood { get; set; }
            public string IdentificationIdentifier { get; set; }
            public string IdentificationDescription { get; set; }
            public bool IdentificationIsCapitalAsset { get; set; }
            public string IdentificationAccumulatedDepreciationAccountCode { get; set; }
            public string IdentificationAccumulatedDepreciationAccountDescription { get; set; }
            public string IdentificationEndowmentAccountCode { get; set; }
            public string IdentificationEndowmentAccountDescription { get; set; }
            public DateTime IdentificationAcquisitionDate { get; set; }
            public decimal IdentificationAcquisitionValue { get; set; }
            public string IdentificationInvoiceNumber { get; set; }
            public string IdentificationPartnerName { get; set; }
            public string IdentificationVatNumber { get; set; }
            public string IdentificationPartnerAccount { get; set; }
            public string IdentificationPostalCode { get; set; }
            public int IdentificationVatType { get; set; }
            public string IdentificationCountryCode { get; set; }
            public decimal? IdentificationBaseAmount { get; set; }
            public string IdentificationTaxCode { get; set; }
            public decimal? IdentificationTaxAmount { get; set; }
            public bool IdentificationProrateApply { get; set; }
            public decimal? IdentificationProrateAmountValue { get; set; }
            public decimal? IdentificationDeductibleAmountValue { get; set; }
            public DateTime? IdentificationAssetEndDate { get; set; }
            public int? IdentificationRetirementReason { get; set; }
            public DateTime RepaymentDataAssetStartDate { get; set; }
            public decimal? RepaymentDataResidualValue { get; set; }
            public int RepaymentDataDepreciationYears { get; set; }
            public int RepaymentDataFiscalDepreciationYears { get; set; }
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