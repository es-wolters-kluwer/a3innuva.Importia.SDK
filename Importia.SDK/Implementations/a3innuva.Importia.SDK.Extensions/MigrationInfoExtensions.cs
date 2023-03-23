namespace a3innuva.TAA.Migration.SDK.Extensions
{
	using System.Collections.Generic;
	using a3innuva.TAA.Migration.SDK.Implementations;
	using a3innuva.TAA.Migration.SDK.Interfaces;

	public static class MigrationInfoExtensions
	{
		public static IEnumerable<IValidationResult> GetValidations(this IMigrationInfo info)
		{
			var infoValidation = new MigrationInfoValidation(info);
			return infoValidation.GetErrorValidations();
		}
	}
}

