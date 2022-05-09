namespace a3innuva.TAA.Migration.SDK.Implementations.Tests
{
    using FluentAssertions;
    using Xunit;

    [Trait("Unit test", "Account")]
    public class AccountTest
    {
        [Fact(DisplayName = "Check identity succeed")]
        public void Check_identity_succeed()
        {
            string param = "identity";

            var entity = new Account()
            {
                Code = param
            };

            entity.Identity().Should().Be(param);
        }
    }
}
