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

		public IEnumerable<IValidationResult> GetErrorValidations()
		{
			ApplyValidations();
			if (IsValid) return Enumerable.Empty<IValidationResult>();

			var listErrors = BuildErrorMessages()
				.Select(x => new ValidationResult()
				{
					Code = x.errorMessage,
					Line = 0,
					IsValid = false
				});
			
			return listErrors;
		}

		private IEnumerable<(bool isInvalid, string errorMessage)> BuildErrorMessages()
		{
			return new List<(bool isInvalid, string errorMessage)>
			{
				( !isValidOrigin, "The origin value is invalid" ),
				( !isValidType, "The type value is invalid" ),
				( !isValidYear, "The year value is invalid" ),
				( !isValidVatNumber, "The VatNumber value is invalid" ),
				( !isValidVersion, "The Version value is invalid")
			}.Where(x => x.isInvalid);
		}

		private void ApplyValidations()
		{
			isValidOrigin = info.Origin != MigrationOrigin.None && Enum.IsDefined(typeof(MigrationOrigin), info.Origin);
			isValidType = info.Type != MigrationType.None && Enum.IsDefined(typeof(MigrationType), info.Type);
			isValidYear = info.Type == MigrationType.ChartOfAccount ? info.Year == 0 : info.Year != 0;
			isValidVatNumber = !string.IsNullOrEmpty(info.VatNumber?.Trim());
			isValidVersion = info.Version == "2.0";
		}
	}
}