namespace a3innuva.TAA.Migration.SDK.Extensions
{
	using System.Linq;
	using System.Collections.Generic;
	using a3innuva.TAA.Migration.SDK.Implementations;
	using a3innuva.TAA.Migration.SDK.Interfaces;

	public static class MigrationInfoExtensions
	{
		public static IEnumerable<IValidationResult> GetValidations(this IMigrationInfo info)
		{
			if (info.IsValid()) return Enumerable.Empty<IValidationResult>();

			var listErrors = new List<IValidationResult>();
			
			if (!info.IsValidOrigin()) listErrors.Add(BuildValidationResult($"The {nameof(IMigrationInfo.Origin)} value is invalid"));
			if (!info.IsValidType()) listErrors.Add(BuildValidationResult($"The {nameof(IMigrationInfo.Type)} value is invalid"));
			if (!info.IsValidYear()) listErrors.Add(BuildValidationResult($"The {nameof(IMigrationInfo.Year)} value is invalid"));
			if (!info.IsValidVatNumber()) listErrors.Add(BuildValidationResult($"The {nameof(IMigrationInfo.VatNumber)} value is invalid"));
			if (!info.IsValidVersion()) listErrors.Add(BuildValidationResult($"The {nameof(IMigrationInfo.Version)} value is invalid"));

			return listErrors;
		}

		private static ValidationResult BuildValidationResult(string error) =>
			new ValidationResult()
			{
				Code = error,
				Line = 0,
				IsValid = false
			};
	}
}