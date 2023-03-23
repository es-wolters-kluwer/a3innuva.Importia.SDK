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
			ApplyValidations();
		}

		public bool InfoIsValid => IsValid;

		public IEnumerable<IValidationResult> GetErrorValidations()
		{
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
				( !isValidOrigin, $"The {nameof(IMigrationInfo.Origin)} value is invalid" ),
				( !isValidType, $"The {nameof(IMigrationInfo.Type)} value is invalid" ),
				( !isValidYear, $"The {nameof(IMigrationInfo.Year)} value is invalid" ),
				( !isValidVatNumber, $"The {nameof(IMigrationInfo.VatNumber)} value is invalid" ),
				( !isValidVersion, $"The {nameof(IMigrationInfo.Version)} value is invalid")
			}.Where(x => x.isInvalid);
		}

		private void ApplyValidations()
		{
			isValidOrigin = info.IsValidOrigin();
			isValidType = info.IsValidType();
			isValidYear = info.IsValidYear();
			isValidVatNumber = info.IsValidVatNumber();
			isValidVersion = info.IsValidVersion();
		}
	}
}