using System;
using System.Collections.Generic;
using System.Linq;
using a3innuva.TAA.Migration.SDK.Interfaces;

namespace a3innuva.TAA.Migration.SDK.Implementations
{
	public class MigrationInfoValidation
	{
		private readonly IMigrationInfo info;
		private bool isValidType;
		private bool isValidOrigin;
		private bool isValidYear;
		private bool isValidVatNumber;
		private bool isValidVersion;

		private bool IsValid =>
			isValidType &&
			isValidOrigin &&
			isValidYear &&
			isValidVatNumber &&
			isValidVersion;

		public MigrationInfoValidation(IMigrationInfo info)
		{
			this.info = info;
		}

		public bool InfoIsValid
		{
			get
			{
				ApplyValidations();
				return IsValid;
			}
		}

		private void ApplyValidations()
		{
			isValidOrigin = info.Origin != MigrationOrigin.None && Enum.IsDefined(typeof(MigrationOrigin), info.Origin);
			isValidType = info.Type != MigrationType.None && Enum.IsDefined(typeof(MigrationType), info.Type);
			isValidYear = info.Type == MigrationType.ChartOfAccount ? info.Year == 0 : info.Year != 0;
			isValidVatNumber = !string.IsNullOrEmpty(info.VatNumber?.Trim());
			isValidVersion = info.Version == "2.0";
		}

		public IEnumerable<IValidationResult> GetValidationResults()
		{
			var validationResults = new List<IValidationResult>();
			ApplyValidations();
			if (IsValid) return validationResults;
			
			if (!isValidOrigin)
				validationResults.Add(new ValidationResult()
				{
					Code = "The origin value is invalid",
					Line = 0,
					IsValid = false
				});
			if (!isValidType)
				validationResults.Add(new ValidationResult()
				{
					Code = "The type value is invalid",
					Line = 0,
					IsValid = false
				});
			
			return validationResults;
		}
	}
}