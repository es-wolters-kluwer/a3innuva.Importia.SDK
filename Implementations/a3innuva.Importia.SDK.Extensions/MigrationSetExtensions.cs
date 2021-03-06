﻿namespace a3innuva.TAA.Migration.SDK.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using a3innuva.TAA.Migration.SDK.Implementations;
    using a3innuva.TAA.Migration.SDK.Interfaces;

    public static class MigrationSetExtensions
    {
        public static (bool isValid, IEnumerable<IValidationResult> errors) IsValid(this IMigrationSet set)
        {
            var errors = new List<IValidationResult>();

            bool isValidSet = set.Info != null && set.Entities != null;

            if (isValidSet == false)
                return (false, errors);

            var isValidInfo = set.Info.IsValid();

            switch (set.Info?.Type)
            {
                case MigrationType.ChartOfAccount:
                    {
                        var validation = new AccountValidation();
                        return Validate(validation, set.Entities.Cast<IAccount>(), isValidInfo);
                    }
                case MigrationType.Journal:
                    {
                        var validation = new JournalValidation();
                        return Validate(validation, set.Entities.Cast<IJournal>(), isValidInfo);
                    }
                case MigrationType.InputInvoice:
                    {
                        var validation = new InputInvoiceValidation();
                        return Validate(validation, set.Entities.Cast<IInputInvoice>(), isValidInfo);
                    }
                case MigrationType.OutputInvoice:
                    {
                        var validation = new OutputInvoiceValidation();
                        return Validate(validation, set.Entities.Cast<IOutputInvoice>(), isValidInfo);
                    }
                case MigrationType.None:
                    return (false, errors);
                default:
                    return (false, errors);
            }
        }

        public static bool IsValid(this IMigrationInfo info)
        {
            var type = info.Type != MigrationType.None;
            var defineType = Enum.IsDefined(typeof(MigrationType), info.Type);
            var origin = info.Origin != MigrationOrigin.None;
            var defineOrigin = Enum.IsDefined(typeof(MigrationOrigin), info.Origin);
            var year = info.Type == MigrationType.ChartOfAccount ? info.Year == 0 : info.Year != 0;
            var vatNumber = !String.IsNullOrEmpty(info.VatNumber?.Trim());
            var version = info.Version == "2.0";

            return type & defineType & origin & defineOrigin & year & vatNumber & version;
        }

        public static bool ValidateTypeAndContent(this IMigrationSet set)
        {
            switch (set.Info.Type)
            {
                case MigrationType.ChartOfAccount:
                    return set.Entities.ToList().TrueForAll(x => x.GetType() == typeof(Account));
                case MigrationType.Journal:
                    return set.Entities.ToList().TrueForAll(x => x.GetType() == typeof(Journal));
                case MigrationType.InputInvoice:
                    return set.Entities.ToList().TrueForAll(x => x.GetType() == typeof(InputInvoice));
                case MigrationType.OutputInvoice:
                    return set.Entities.ToList().TrueForAll(x => x.GetType() == typeof(OutputInvoice));
                default:
                    return false;
            }
        }

        private static (bool valid, IEnumerable<IValidationResult> errors) Validate<T>(IValidation<T> validator, IEnumerable<T> items, bool isValidInfo)
            where T : class, IMigrationEntity
        {
            List<IValidationResult> errors = new List<IValidationResult>();

            foreach (var item in items)
            {
                var result = validator.Validate(item);

                if (result.Any())
                    errors.AddRange(result.Where(x => x.IsValid == false));
            }

            return (!errors.Any() && isValidInfo, errors);
        }
    }
}
