namespace a3innuva.TAA.Migration.SDK.Extensions.Tests
{
    using FluentAssertions;
    using Implementations;
    using Interfaces;
    using Xunit;

    [Trait("Unit test", "JournalExtensions")]
    public class JournalExtensionsTests
    {

        [Fact(DisplayName = "HasAmount false")]
        public void HasAmount_false()
        {
            var entity = this.GetEntity();

            entity.HasAmount().Should().BeFalse();
        }

        [Fact(DisplayName = "HasAmount with debit true")]
        public void HasAmount_debit_true()
        {
            var entity = this.GetEntity();
            entity.Debit = 1;

            entity.HasAmount().Should().BeTrue();
        }

        [Fact(DisplayName = "HasAmount with credit true")]
        public void HasAmount_credit_true()
        {
            var entity = this.GetEntity();
            entity.Credit = 1;

            entity.HasAmount().Should().BeTrue();
        }

        [Fact(DisplayName = "HasAmount with debit and credit true")]
        public void HasAmount_debit_credit_true()
        {
            var entity = this.GetEntity();
            entity.Credit = 1;
            entity.Debit = 1;

            entity.HasAmount().Should().BeTrue();
        }

        [Fact(DisplayName = "IsAmountOnDebit false")]
        public void IsAmountOnDebit_false()
        {
            var entity = this.GetEntity();
            entity.Debit = 0;

            entity.IsAmountOnDebit().Should().BeFalse();
        }

        [Fact(DisplayName = "IsAmountOnDebit true")]
        public void IsAmountOnDebit_true()
        {
            var entity = this.GetEntity();
            entity.Debit = 1;

            entity.IsAmountOnDebit().Should().BeTrue();
        }

        [Fact(DisplayName = "IsAmountOnCredit false")]
        public void IsAmountOnCredit_false()
        {
            var entity = this.GetEntity();
            entity.Credit = 0;

            entity.IsAmountOnCredit().Should().BeFalse();
        }

        [Fact(DisplayName = "IsAmountOnCredit true")]
        public void IsAmountOnCredit_true()
        {
            var entity = this.GetEntity();
            entity.Credit = 1;

            entity.IsAmountOnCredit().Should().BeTrue();
        }

        [Fact(DisplayName = "Get Amount with credit")]
        public void Get_Amount_with_credit()
        {
            var entity = this.GetEntity();
            entity.Credit = 1;

            entity.Amount().Should().Be(entity.Credit);
        }

        [Fact(DisplayName = "Get Amount with debit")]
        public void Get_Amount_with_debit()
        {
            var entity = this.GetEntity();
            entity.Debit = 1;

            entity.Amount().Should().Be(entity.Debit);
        }

        [Fact(DisplayName = "Get Amount with debit and credit")]
        public void Get_Amount_with_debit_and_credit()
        {
            var entity = this.GetEntity();
            entity.Debit = 1;
            entity.Credit = 2;

            entity.Amount().Should().Be(entity.Debit);
        }

        private IJournalLine GetEntity()
        {
            return new JournalLine();
        }
    }
}
