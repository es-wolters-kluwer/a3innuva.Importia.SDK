namespace a3innuva.TAA.Migration.SDK.Serialization.Tests
{
    using System;
    using System.Collections.Generic;
    using a3innuva.TAA.Migration.SDK.Implementations;
    using a3innuva.TAA.Migration.SDK.Interfaces;
    using FluentAssertions;
    using Newtonsoft.Json;
    using Xunit;

    public class SerializationTests
    {
        [Fact]
        public void Test1()
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

            var json = JsonConvert.SerializeObject(set,
                JsonSerializationSettingsUtils.GetSettings(Formatting.Indented));

            var result =
                JsonConvert.DeserializeObject<IMigrationSet>(json, JsonSerializationSettingsUtils.GetSettings(Formatting.Indented));

            result.Info.VatNumber.Should().Be(info.VatNumber);

        }
    }
}
