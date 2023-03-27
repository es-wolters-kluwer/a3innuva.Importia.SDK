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
			
			if (!info.IsKnowOrigin()) listErrors.Add(BuildValidationResult($"Unknown migration {nameof(IMigrationInfo.Origin).ToLower()}"));
			if (!info.IsDefinedOrigin()) listErrors.Add(BuildValidationResult($"Migration {nameof(IMigrationInfo.Origin).ToLower()} is not defined"));
			if (!info.IsKnowType()) listErrors.Add(BuildValidationResult($"Unknown migration {nameof(IMigrationInfo.Type).ToLower()}"));
			if (!info.IsDefinedType()) listErrors.Add(BuildValidationResult($"Migration {nameof(IMigrationInfo.Type).ToLower()} is not defined"));
			if (!info.IsValidYear()) listErrors.Add(BuildValidationResult($"{nameof(IMigrationInfo.Year)} is not defined"));
			if (!info.IsValidVatNumber()) listErrors.Add(BuildValidationResult($"Vat Number is not defined"));
			if (!info.IsValidVersion()) listErrors.Add(BuildValidationResult($"Migration number {nameof(IMigrationInfo.Version).ToLower()} erroneous"));

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