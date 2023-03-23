using System;
using System.Collections.Generic;
using System.Linq;
using a3innuva.TAA.Migration.SDK.Interfaces;

namespace a3innuva.TAA.Migration.SDK.Implementations
{
	public class MigrationInfoValidation
	{
		private readonly IMigrationInfo info;

		public MigrationInfoValidation(IMigrationInfo info)
		{
			this.info = info;
		}

		public IEnumerable<IValidationResult> GetErrorValidations()
		{
			if (info.IsValid()) return Enumerable.Empty<IValidationResult>();

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
				( !info.IsValidOrigin(), $"The {nameof(IMigrationInfo.Origin)} value is invalid" ),
				( !info.IsValidType(), $"The {nameof(IMigrationInfo.Type)} value is invalid" ),
				( !info.IsValidYear(), $"The {nameof(IMigrationInfo.Year)} value is invalid" ),
				( !info.IsValidVatNumber(), $"The {nameof(IMigrationInfo.VatNumber)} value is invalid" ),
				( !info.IsValidVersion(), $"The {nameof(IMigrationInfo.Version)} value is invalid")
			}.Where(x => x.isInvalid);
		}
	}
}