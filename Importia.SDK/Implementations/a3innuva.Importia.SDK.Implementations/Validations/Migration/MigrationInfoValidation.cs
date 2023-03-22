using System;
using a3innuva.TAA.Migration.SDK.Interfaces;

namespace a3innuva.TAA.Migration.SDK.Implementations
{
	public class MigrationInfoValidation
	{
		private readonly IMigrationInfo info;
		public bool IsValidType {get; private set; }
		public bool IsValidDefineType {get; private set; }
		public bool IsValidOrigin {get; private set; }
		public bool IsValidDefineOrigin {get; private set; }
		public bool IsValidYear {get; private set; }
		public bool IsValidVatNumber {get; private set; }
		public bool IsValidVersion {get; private set; }
		public MigrationInfoValidation(IMigrationInfo info)
		{
			this.info = info;
		}

		public void ApplyValidations()
		{
			IsValidType = info.Type != MigrationType.None;
			IsValidDefineType = Enum.IsDefined(typeof(MigrationType), info.Type);
			IsValidOrigin = info.Origin != MigrationOrigin.None;
			IsValidDefineOrigin = Enum.IsDefined(typeof(MigrationOrigin), info.Origin);
			IsValidYear = info.Type == MigrationType.ChartOfAccount ? info.Year == 0 : info.Year != 0;
			IsValidVatNumber = !String.IsNullOrEmpty(info.VatNumber?.Trim());
			IsValidVersion = info.Version == "2.0";
		}
	}
}