namespace a3innuva.TAA.Migration.SDK.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using a3innuva.TAA.Migration.SDK.Implementations;
    using a3innuva.TAA.Migration.SDK.Interfaces;

    public static class MigrationSetExtensions
    {
        public static (bool isValid, IEnumerable<IValidationResult> errors) IsValid(this IMigrationSet set)
        {
            var errors = new List<IValidationResult>();

            bool isValidSet = set.Info != null && set.Entities != null;

            if (!isValidSet)
                return (false, errors);

            var isValidInfo = set.Info.IsValid();

            switch (set.Info?.Type)
            {
                case MigrationType.ChartOfAccount:
                    {
                        var accountValidation = new AccountValidation();
                        var accountEntity = set.Entities.Cast<IAccount>();
                        return Validate(accountValidation, accountEntity, isValidInfo);
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
                case MigrationType.Activity:
                    {
                        var validation = new ActivityValidation();
                        return Validate(validation, set.Entities.Cast<IActivity>(), isValidInfo);
                    }
                case MigrationType.Channel:
                {
                    var validation = new ChannelValidation();
                    return Validate(validation, set.Entities.Cast<IChannel>(), isValidInfo);
                }
                case MigrationType.None:
                    return (false, errors);
                default:
                    return (false, errors);
            }
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

            var supplierValidation = new SupplierValidation();
            var clientValidation = new ClientValidation();
            foreach (var item in items)
            {
                var result = new List<IValidationResult>(validator.Validate(item));
                if (typeof(T) == typeof(IAccount))
                {
                    var account = (IAccount)item;
                    if ((account.IsAPartner() || account.Partner.IsAPartner()) && account.Partner != null)
                    {
                        if ((account.Partner.MaturitiesAccountCode ?? account.Code).StartsWith("700"))
                        {
                            result.AddRange(supplierValidation.Validate(account.Partner));
                        }
                        else
                        {
                            result.AddRange(clientValidation.Validate(account.Partner));
                        }
                    }
                }

                if (result.Any())
                    errors.AddRange(result.Where(x => !x.IsValid));
            }

            return (!errors.Any() && isValidInfo, errors);
        }
    }
}
