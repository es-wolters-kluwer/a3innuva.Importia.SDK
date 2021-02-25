namespace a3innuva.TAA.Migration.SDK.Extensions.Tests
{
    using FluentAssertions;
    using Implementations;
    using Interfaces;
    using Xunit;

    [Trait("Unit test", "AccountExtensions")]
    public class AccountExtensionsTests
    {
        [Fact(DisplayName = "HasVatNumber false")]
        public void HasVatNumber_false()
        {
            var entity = this.GetEntity();

            entity.HasVatNumber().Should().BeFalse();
        }

        [Fact(DisplayName = "HasVatNumber true")]
        public void HasVatNumber_true()
        {
            var entity = this.GetEntity();
            entity.VatNumber = "vatNumber";

            entity.HasVatNumber().Should().BeTrue();
        }

        [Fact(DisplayName = "HasName false")]
        public void HasName_false()
        {
            var entity = this.GetEntity();

            entity.HasName().Should().BeFalse();
        }

        [Fact(DisplayName = "HasName true")]
        public void HasName_true()
        {
            var entity = this.GetEntity();
            entity.Name = "name";

            entity.HasName().Should().BeTrue();
        }

        [Fact(DisplayName = "HasPostalCode false")]
        public void HasPostalCode_false()
        {
            var entity = this.GetEntity();

            entity.HasPostalCode().Should().BeFalse();
        }

        [Fact(DisplayName = "HasPostalCode true")]
        public void HasPostalCode_true()
        {
            var entity = this.GetEntity();
            entity.PostalCode = "08000";

            entity.HasPostalCode().Should().BeTrue();
        }

        [Fact(DisplayName = "IsAPerson false")]
        public void IsAPerson_false()
        {
            var entity = this.GetEntity();

            entity.IsAPerson().Should().BeFalse();
        }

        [Fact(DisplayName = "IsAPerson only with name false")]
        public void IsAPerson_only_with_name_false()
        {
            var entity = this.GetEntity();
            entity.Name = "name";

            entity.IsAPerson().Should().BeFalse();
        }

        [Fact(DisplayName = "IsAPerson only with vatNumber false")]
        public void IsAPerson_only_with_vatNumber_false()
        {
            var entity = this.GetEntity();
            entity.VatNumber = "vatNumber";

            entity.IsAPerson().Should().BeFalse();
        }

        [Fact(DisplayName = "IsAPerson true")]
        public void IsAPerson_true()
        {
            var entity = this.GetEntity();
            entity.VatNumber = "vatNumber";
            entity.Name = "name";

            entity.IsAPerson().Should().BeTrue();
        }

        [Fact(DisplayName = "IsAPartner false")]
        public void IsAPartner_false()
        {
            var entity = this.GetEntity();

            entity.IsAPartner().Should().BeFalse();
        }


        [Fact(DisplayName = "IsAPartner true")]
        public void IsAPartner_true()
        {
            var entity = this.GetEntity();
            entity.Name = "name";

            entity.IsAPartner().Should().BeTrue();
        }

        private IAccount GetEntity()
        {
            return new Account()
            {
                Code = "4300000",
                Description = "Desc"
            };
        }
    }
}
