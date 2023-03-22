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
		private bool isValidDefineType;
		private bool isValidOrigin;
		private bool isValidDefineOrigin;
		private bool isValidYear;
		private bool isValidVatNumber;
		private bool isValidVersion;

		private bool IsValid =>
			isValidType &&
			isValidDefineType &&
			isValidOrigin &&
			isValidDefineOrigin &&
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
			isValidType = info.Type != MigrationType.None;
			isValidDefineType = Enum.IsDefined(typeof(MigrationType), info.Type);
			isValidOrigin = info.Origin != MigrationOrigin.None;
			isValidDefineOrigin = Enum.IsDefined(typeof(MigrationOrigin), info.Origin);
			isValidYear = info.Type == MigrationType.ChartOfAccount ? info.Year == 0 : info.Year != 0;
			isValidVatNumber = !string.IsNullOrEmpty(info.VatNumber?.Trim());
			isValidVersion = info.Version == "2.0";
		}
	}
}