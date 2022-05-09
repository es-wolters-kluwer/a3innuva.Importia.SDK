namespace a3innuva.TAA.Migration.SDK.Implementations.Tests
{
    using FluentAssertions;
    using Xunit;

    [Trait("Unit test", "JournalLine")]
    public class JournalLineTest
    {
        [Fact(DisplayName = "Check identity succeed")]
        public void Check_identity_succeed()
        {
            string param2 = "param2";

            var entity = new JournalLine()
            {
                Number = param2
            };

            entity.Identity().Should().Be($"{param2}");
        }
    }
}
