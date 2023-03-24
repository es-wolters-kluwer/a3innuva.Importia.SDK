using System.Collections.Generic;
using a3innuva.TAA.Migration.SDK.Implementations;
using a3innuva.TAA.Migration.SDK.Interfaces;
using FluentAssertions;
using Xunit;

namespace a3innuva.TAA.Migration.SDK.Extensions.Tests;

public class MigrationInfoExtensionsTests
{
	[Fact]
	public void ShouldReturnTrueWhenInfoIsValid()
	{
		IMigrationInfo info = new MigrationInfo()
		{
			Origin = MigrationOrigin.Eco,
			Type = MigrationType.ChartOfAccount,
			Year = 0,
			VatNumber = "vatNumber",
			Version = "2.0"
		};

		var validationResults = info.GetValidations();

		validationResults.Should().BeEmpty();
	}

	[Fact]
	public void ShouldReturnErrorsValidationIfInfoIsInvalid()
	{
		IMigrationInfo info = new MigrationInfo()
		{
			Origin = (MigrationOrigin)1,
			Type = MigrationType.ChartOfAccount,
			Year = 0,
			VatNumber = "vatNumber",
			Version = "2.0"
		};

		var validationResults = info.GetValidations();

		validationResults.Should().HaveCountGreaterThan(0);
	}

	[Fact]
	public void ShouldReturnMultipleValidationErrors()
	{
		var infoGiven = new MigrationInfo()
		{
			Origin = (MigrationOrigin) 1,
			Type = (MigrationType) 99,
			Year = 2022,
			VatNumber = "",
			Version = "1.0"
		};

		var validationResults = infoGiven.GetValidations();

		var validationResultExpected = new List<IValidationResult>()
		{
			new ValidationResult { Code = "The Origin value is invalid", IsValid = false, Line = 0 },
			new ValidationResult { Code = "The Type value is invalid", IsValid = false, Line = 0 },
			new ValidationResult { Code = "The VatNumber value is invalid", IsValid = false, Line = 0 },
			new ValidationResult { Code = "The Version value is invalid", IsValid = false, Line = 0 },
		};
            
		validationResults.Should().BeEquivalentTo(validationResultExpected);
	}

	[Theory]
	[MemberData(nameof(ShouldReturnErrorsValidationScenaries), MemberType = typeof(MigrationInfoExtensionsTests))]
	public void ShouldReturnErrorsValidation(IMigrationInfo infoGiven, ValidationResult validationResultExpected)
	{
		var validationResults = infoGiven.GetValidations();

		validationResults.Should().BeEquivalentTo(new List<IValidationResult> { validationResultExpected });
	}

	public static TheoryData<IMigrationInfo, ValidationResult> ShouldReturnErrorsValidationScenaries =>
		new TheoryData<IMigrationInfo, ValidationResult>
		{
			{
				new MigrationInfo
				{
					Origin = MigrationOrigin.None,
					Type = MigrationType.Journal,
					Year = 2022,
					VatNumber = "vatNumber",
					Version = "2.0"
				},
				new ValidationResult
				{
					Code = "The Origin value is invalid",
					Line = 0,
					IsValid = false
				}
			},
			{
				new MigrationInfo()
				{
					Origin = MigrationOrigin.Eco,
					Type = MigrationType.None, 
					Year = 2022, 
					VatNumber = "vatNumber",
					Version = "2.0"
				},
				new ValidationResult()
				{
					Code = "The Type value is invalid",
					Line = 0,
					IsValid = false
				}
			},
			{
				new MigrationInfo()
				{
					Origin = MigrationOrigin.Eco,
					Type = MigrationType.ChartOfAccount, 
					Year = 10, 
					VatNumber = "vatNumber",
					Version = "2.0"
				},
				new ValidationResult()
				{
					Code = "The Year value is invalid",
					Line = 0,
					IsValid = false
				}
			},
			{
				new MigrationInfo()
				{
					Origin = MigrationOrigin.Eco,
					Type = MigrationType.Journal, 
					Year = 2022, 
					VatNumber = string.Empty,
					Version = "2.0"
				},
				new ValidationResult()
				{
					Code = "The VatNumber value is invalid",
					Line = 0,
					IsValid = false
				}
			},
			{
				new MigrationInfo()
				{
					Origin = MigrationOrigin.Eco,
					Type = MigrationType.Journal, 
					Year = 2022, 
					VatNumber = "vatNumber",
					Version = "1.0"
				},
				new ValidationResult()
				{
					Code = "The Version value is invalid",
					Line = 0,
					IsValid = false
				}
			}
		};
}