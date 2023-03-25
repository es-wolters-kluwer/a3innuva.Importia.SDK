using System.Collections.Generic;
using a3innuva.TAA.Migration.SDK.Implementations;
using a3innuva.TAA.Migration.SDK.Interfaces;
using FluentAssertions;
using Xunit;

namespace a3innuva.TAA.Migration.SDK.Extensions.Tests;

public class MigrationInfoExtensionsTests
{
	private const string VersionValueErrorMessage = "Migration number version erroneous";
	private const string VatNumberValueErrorMessage = "Vat Number is not defined";
	private const string YearValueErrorMessage = "Year is not defined";
	private const string TypeValueUnknownErrorMessage = "Unknown migration type";
	private const string TypeValueUndefinedErrorMessage = "Migration type is not defined";
	private const string OriginValueUnknownErrorMessage = "Unknown migration origin";
	private const string OriginValueUndefinedErrorMessage = "Migration origin is not defined";

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
			new ValidationResult { Code = OriginValueUnknownErrorMessage, IsValid = false, Line = 0 },
			new ValidationResult { Code = TypeValueUnknownErrorMessage, IsValid = false, Line = 0 },
			new ValidationResult { Code = VatNumberValueErrorMessage, IsValid = false, Line = 0 },
			new ValidationResult { Code = VersionValueErrorMessage, IsValid = false, Line = 0 },
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
					Code = OriginValueUndefinedErrorMessage,
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
					Code = TypeValueUndefinedErrorMessage,
					Line = 0,
					IsValid = false
				}
			},
			{
				new MigrationInfo()
				{
					Origin = MigrationOrigin.Eco,
					Type = (MigrationType) 9, 
					Year = 2022, 
					VatNumber = "vatNumber",
					Version = "2.0"
				},
				new ValidationResult()
				{
					Code = TypeValueUnknownErrorMessage,
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
					Code = YearValueErrorMessage,
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
					Code = VatNumberValueErrorMessage,
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
					Code = VersionValueErrorMessage,
					Line = 0,
					IsValid = false
				}
			}
		};
}