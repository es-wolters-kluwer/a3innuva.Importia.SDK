﻿using System;

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

        public bool IsValidOrigin() => IsDefinedOrigin() && IsKnowOrigin();

        public bool IsKnowOrigin() => Enum.IsDefined(typeof(MigrationOrigin), Origin);

        public bool IsDefinedOrigin() => Origin != MigrationOrigin.None;

        public bool IsValidType() => IsDefinedType() && IsKnowType();

        public bool IsDefinedType() => Type != MigrationType.None;

        public bool IsKnowType() => Enum.IsDefined(typeof(MigrationType), Type);

        public bool IsValidYear() => Type == MigrationType.ChartOfAccount ? Year == 0 : Year != 0;

        public bool IsValidVatNumber() => !string.IsNullOrEmpty(VatNumber?.Trim());

        public bool IsValidVersion() => Version == "2.0";

        public bool IsValid() =>
            IsValidType() &&
            IsValidOrigin() &&
            IsValidYear() &&
            IsValidVatNumber() &&
            IsValidVersion();
    }
}
