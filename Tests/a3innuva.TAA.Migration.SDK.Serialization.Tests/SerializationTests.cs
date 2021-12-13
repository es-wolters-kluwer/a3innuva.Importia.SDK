namespace a3innuva.TAA.Migration.SDK.Serialization.Tests
{
    using System;
    using System.Collections.Generic;
    using a3innuva.TAA.Migration.SDK.Implementations;
    using a3innuva.TAA.Migration.SDK.Interfaces;
    using FluentAssertions;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using Xunit;

    [Trait("Unit test", "Serialization")]
    public class SerializationTests
    {
        [Fact(DisplayName = "Serialize and deserialize a migration set")]
        public void Serialize_and_deserialize_a_migration_set()
        {
            var set = this.CreateMigrationSet();

            var json = JsonConvert.SerializeObject(set,
                JsonSerializationSettingsUtils.GetSettings(Formatting.Indented));

            var result =
                JsonConvert.DeserializeObject<IMigrationSet>(json, JsonSerializationSettingsUtils.GetSettings(Formatting.Indented));

            result.Info.VatNumber.Should().Be(set.Info.VatNumber);

        }

        [Fact(DisplayName = "Serialize with default serialization binder and deserialize with a3innuvaSerializationBinder")]
        public void Serialize_with_default_serialization_binder_and_deserialize_with_a3innuvaSerializationBinder()
        {
            var set = this.CreateMigrationSet();

            var jsonSettingsSerialize = JsonSerializationSettingsUtils.GetSettings(Formatting.Indented);
            jsonSettingsSerialize.SerializationBinder = new DefaultSerializationBinder();

            var jsonSettingsDeserialize = JsonSerializationSettingsUtils.GetSettings(Formatting.Indented);
            jsonSettingsSerialize.SerializationBinder = new a3innuvaSerializationBinder();

            var json = JsonConvert.SerializeObject(set,jsonSettingsSerialize);
            
            var result = JsonConvert.DeserializeObject<IMigrationSet>(json, jsonSettingsDeserialize);

            result.Info.VatNumber.Should().Be(set.Info.VatNumber);

        }

        [Fact(DisplayName = "Serialize with a3innuvaSerializationBinder  and deserialize with default serialization binder")]
        public void Serialize_with_a3innuvaSerializationBinder_and_deserialize_with_default_serialization_binder()
        {
            var set = this.CreateMigrationSet();

            var jsonSettingsSerialize = JsonSerializationSettingsUtils.GetSettings(Formatting.Indented);
            jsonSettingsSerialize.SerializationBinder =  new a3innuvaSerializationBinder();

            var jsonSettingsDeserialize = JsonSerializationSettingsUtils.GetSettings(Formatting.Indented);
            jsonSettingsSerialize.SerializationBinder = new a3innuvaSerializationBinder();

            var json = JsonConvert.SerializeObject(set,jsonSettingsSerialize);

            var result = JsonConvert.DeserializeObject<IMigrationSet>(json, jsonSettingsDeserialize);

            result.Info.VatNumber.Should().Be(set.Info.VatNumber);

        }

        [Fact(DisplayName = "Serialize and deserialize a migration set with TypeNameAssemblyFormatHandling.Full ")]
        public void Serialize_and_deserialize_a_migration_set_with_TypeNameAssemblyFormatHandling_Full()
        {
            var set = this.CreateMigrationSet();

            var jsonSettingsSerialize = JsonSerializationSettingsUtils.GetSettings(Formatting.Indented);
            jsonSettingsSerialize.TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Full;

            var json = JsonConvert.SerializeObject(set, jsonSettingsSerialize);

            var result = JsonConvert.DeserializeObject<IMigrationSet>(json, jsonSettingsSerialize);

            result.Info.VatNumber.Should().Be(set.Info.VatNumber);

        }

        [Fact(DisplayName = "Serialize with TypeNameAssemblyFormatHandling.Full and deserialize with TypeNameAssemblyFormatHandling.Simple")]
        public void Serialize_with_TypeNameAssemblyFormatHandling_Full_and_deserialize_with_TypeNameAssemblyFormatHandling_Simple()
        {
            var set = this.CreateMigrationSet();

            var jsonSettingsSerialize = JsonSerializationSettingsUtils.GetSettings(Formatting.Indented);
            jsonSettingsSerialize.TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Full;

            var jsonSettingsDeserialize = JsonSerializationSettingsUtils.GetSettings(Formatting.Indented);
            jsonSettingsSerialize.TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple;

            var json = JsonConvert.SerializeObject(set,jsonSettingsSerialize);

            var result = JsonConvert.DeserializeObject<IMigrationSet>(json, jsonSettingsDeserialize);

            result.Info.VatNumber.Should().Be(set.Info.VatNumber);

        }

        [Fact(DisplayName = "Serialize with TypeNameAssemblyFormatHandling.Simple and deserialize with TypeNameAssemblyFormatHandling.Full")]
        public void Serialize_with_TypeNameAssemblyFormatHandling_Simple_and_deserialize_with_TypeNameAssemblyFormatHandling_Full()
        {
            var set = this.CreateMigrationSet();

            var jsonSettingsSerialize = JsonSerializationSettingsUtils.GetSettings(Formatting.Indented);
            jsonSettingsSerialize.TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple;

            var jsonSettingsDeserialize = JsonSerializationSettingsUtils.GetSettings(Formatting.Indented);
            jsonSettingsSerialize.TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Full;

            var json = JsonConvert.SerializeObject(set,jsonSettingsSerialize);

            var result = JsonConvert.DeserializeObject<IMigrationSet>(json, jsonSettingsDeserialize);

            result.Info.VatNumber.Should().Be(set.Info.VatNumber);

        }

        [Fact(DisplayName = "Serialize and deserialize an outputInvoiceAdditionalData migration set")]
        public void Serialize_and_deserialize_an_outputInvoiceAdditionalData_migration_set()
        {
            var set = this.CreateInvoiceAdditionalDataMigrationSet();

            var json = JsonConvert.SerializeObject(set,
                JsonSerializationSettingsUtils.GetSettings(Formatting.Indented));

            var result =
                JsonConvert.DeserializeObject<IMigrationSet>(json, JsonSerializationSettingsUtils.GetSettings(Formatting.Indented));

            result.Info.VatNumber.Should().Be(set.Info.VatNumber);

        }

        [Fact(DisplayName = "Serialize outputInvoiceAdditionalData with default serialization binder and deserialize with a3innuvaSerializationBinder")]
        public void Serialize_outputInvoiceAdditionalData_with_default_serialization_binder_and_deserialize_with_a3innuvaSerializationBinder()
        {
            var set = this.CreateInvoiceAdditionalDataMigrationSet();

            var jsonSettingsSerialize = JsonSerializationSettingsUtils.GetSettings(Formatting.Indented);
            jsonSettingsSerialize.SerializationBinder = new DefaultSerializationBinder();

            var jsonSettingsDeserialize = JsonSerializationSettingsUtils.GetSettings(Formatting.Indented);
            jsonSettingsSerialize.SerializationBinder = new a3innuvaSerializationBinder();

            var json = JsonConvert.SerializeObject(set, jsonSettingsSerialize);

            var result = JsonConvert.DeserializeObject<IMigrationSet>(json, jsonSettingsDeserialize);

            result.Info.VatNumber.Should().Be(set.Info.VatNumber);

        }

        [Fact(DisplayName = "Serialize outputInvoiceAdditionalData with a3innuvaSerializationBinder  and deserialize with default serialization binder")]
        public void Serialize_outputInvoiceAdditionalData_with_a3innuvaSerializationBinder_and_deserialize_with_default_serialization_binder()
        {
            var set = this.CreateInvoiceAdditionalDataMigrationSet();

            var jsonSettingsSerialize = JsonSerializationSettingsUtils.GetSettings(Formatting.Indented);
            jsonSettingsSerialize.SerializationBinder = new a3innuvaSerializationBinder();

            var jsonSettingsDeserialize = JsonSerializationSettingsUtils.GetSettings(Formatting.Indented);
            jsonSettingsSerialize.SerializationBinder = new a3innuvaSerializationBinder();

            var json = JsonConvert.SerializeObject(set, jsonSettingsSerialize);

            var result = JsonConvert.DeserializeObject<IMigrationSet>(json, jsonSettingsDeserialize);

            result.Info.VatNumber.Should().Be(set.Info.VatNumber);

        }

        [Fact(DisplayName = "Serialize outputInvoiceAdditionalData and deserialize a migration set with TypeNameAssemblyFormatHandling.Full ")]
        public void Serialize_outputInvoiceAdditionalData_and_deserialize_a_migration_set_with_TypeNameAssemblyFormatHandling_Full()
        {
            var set = this.CreateInvoiceAdditionalDataMigrationSet();

            var jsonSettingsSerialize = JsonSerializationSettingsUtils.GetSettings(Formatting.Indented);
            jsonSettingsSerialize.TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Full;

            var json = JsonConvert.SerializeObject(set, jsonSettingsSerialize);

            var result = JsonConvert.DeserializeObject<IMigrationSet>(json, jsonSettingsSerialize);

            result.Info.VatNumber.Should().Be(set.Info.VatNumber);

        }

        [Fact(DisplayName = "Serialize outputInvoiceAdditionalData with TypeNameAssemblyFormatHandling.Full and deserialize with TypeNameAssemblyFormatHandling.Simple")]
        public void Serialize_outputInvoiceAdditionalData_with_TypeNameAssemblyFormatHandling_Full_and_deserialize_with_TypeNameAssemblyFormatHandling_Simple()
        {
            var set = this.CreateInvoiceAdditionalDataMigrationSet();

            var jsonSettingsSerialize = JsonSerializationSettingsUtils.GetSettings(Formatting.Indented);
            jsonSettingsSerialize.TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Full;

            var jsonSettingsDeserialize = JsonSerializationSettingsUtils.GetSettings(Formatting.Indented);
            jsonSettingsSerialize.TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple;

            var json = JsonConvert.SerializeObject(set, jsonSettingsSerialize);

            var result = JsonConvert.DeserializeObject<IMigrationSet>(json, jsonSettingsDeserialize);

            result.Info.VatNumber.Should().Be(set.Info.VatNumber);

        }

        [Fact(DisplayName = "Serialize outputInvoiceAdditionalData with TypeNameAssemblyFormatHandling.Simple and deserialize with TypeNameAssemblyFormatHandling.Full")]
        public void Serialize_outputInvoiceAdditionalData_with_TypeNameAssemblyFormatHandling_Simple_and_deserialize_with_TypeNameAssemblyFormatHandling_Full()
        {
            var set = this.CreateInvoiceAdditionalDataMigrationSet();

            var jsonSettingsSerialize = JsonSerializationSettingsUtils.GetSettings(Formatting.Indented);
            jsonSettingsSerialize.TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple;

            var jsonSettingsDeserialize = JsonSerializationSettingsUtils.GetSettings(Formatting.Indented);
            jsonSettingsSerialize.TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Full;

            var json = JsonConvert.SerializeObject(set, jsonSettingsSerialize);

            var result = JsonConvert.DeserializeObject<IMigrationSet>(json, jsonSettingsDeserialize);

            result.Info.VatNumber.Should().Be(set.Info.VatNumber);

        }

        [Fact(DisplayName = "Check serialization options")]
        public void Check_serialization_options()
        {
            var jsonSettings = JsonSerializationSettingsUtils.GetSettings();

            jsonSettings.Formatting.Should().Be(Formatting.None);
            jsonSettings.NullValueHandling.Should().Be(NullValueHandling.Ignore);
            jsonSettings.TypeNameAssemblyFormatHandling.Should().Be(TypeNameAssemblyFormatHandling.Simple);
            jsonSettings.TypeNameHandling.Should().Be(TypeNameHandling.All);
            jsonSettings.DateParseHandling.Should().Be(DateParseHandling.DateTime);
            jsonSettings.DateTimeZoneHandling.Should().Be(DateTimeZoneHandling.Utc);
            jsonSettings.SerializationBinder.Should().BeAssignableTo<a3innuvaSerializationBinder>();
        }

        private IMigrationSet CreateMigrationSet()
        {
            IMigrationInfo info = new MigrationInfo()
            {
                FileName = "none",
                Origin = MigrationOrigin.A3Factura,
                Type = MigrationType.ChartOfAccount,
                VatNumber = "336548M",
                Version = "2.0"
            };

            List<IMigrationEntity> entities = new List<IMigrationEntity>()
            {
                new Account()
                {
                    Id = Guid.NewGuid(),
                    Description = "desc",
                    Code = "43000000"
                }
            };

            IMigrationSet set = new MigrationSet()
            {
                Info = info,
                Entities = entities.ToArray()
            };

            return set;
        }

        private IMigrationSet CreateInvoiceAdditionalDataMigrationSet()
        {
            IMigrationInfo info = new MigrationInfo()
            {
                FileName = "none",
                Origin = MigrationOrigin.Eco,
                Type = MigrationType.OutputInvoice,
                VatNumber = "336548M",
                Version = "2.0"
            };

            List<IMigrationEntity> entities = new List<IMigrationEntity>()
            {
                new OutputInvoice()
                {
                    Id = Guid.NewGuid(),
                    AdditionalData = new OutputInvoiceAdditionalData(){ 
                        CouponsBonusesOrDiscounts = true,
                        Description = "desc",
                        Fundamental = FundamentalAdditionalDataEnum.IssuedNotCorrectiveGeneralRegime,
                        Id = Guid.NewGuid(),
                        InitialNumberOfDocument = "10",
                        LastNumberOfDocument = "20",
                        InvoiceByThird = true,
                        InvoicesInSpecialRegime = true,
                        MultipleRecipients = true,
                        NotIncludedInCensus = true,
                        TaxableIncomeAtCost = 10,
                        TypeOfDocument = TypeOfDocumentAdditionalDataEnum.IssuedNotCorrectiveInvoiceWithoutObligation
                    }
                }
            };

            IMigrationSet set = new MigrationSet()
            {
                Info = info,
                Entities = entities.ToArray()
            };

            return set;
        }
    }
}
