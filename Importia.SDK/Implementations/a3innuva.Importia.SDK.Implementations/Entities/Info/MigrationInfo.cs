using System;

namespace a3innuva.TAA.Migration.SDK.Implementations
{
    using Interfaces;

    public class MigrationInfo : IMigrationInfo
    {       
        public string CorrelationActivityId { get; set; }
        public string CorrelationChannelId { get; set; }
        public string VatNumber { get; set; }
        public int Year { get; set; }
        public MigrationType Type { get; set; }
        public MigrationOrigin Origin { get; set; }
        public string FileName { get; set; }
        public string Version { get; set; }

        public bool IsValidOrigin()
        {
            return Origin != MigrationOrigin.None && Enum.IsDefined(typeof(MigrationOrigin), Origin);
        }

        public bool IsValidType()
        {
            return Type != MigrationType.None && Enum.IsDefined(typeof(MigrationType), Type);
        }

        public bool IsValidYear()
        {
            return Type == MigrationType.ChartOfAccount ? Year == 0 : Year != 0;
        }

        public bool IsValidVatNumber()
        {
            return !string.IsNullOrEmpty(VatNumber?.Trim());
        }

        public bool IsValidVersion()
        {
            return Version == "2.0";
        }
    }
}
