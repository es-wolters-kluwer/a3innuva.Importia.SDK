namespace a3innuva.TAA.Migration.SDK.Implementations.Tests
{
    using System;
    using FluentAssertions;
    using Xunit;

    [Trait("Unit test", "Journal")]
    public class JournalTest
    {
        [Fact(DisplayName = "Check identity succeed")]
        public void Check_identity_succeed()
        {
            DateTime param1 = DateTime.UtcNow;
            string param2 = "param2";

            var entity = new Journal()
            {
                Date = param1,
                Number = param2
            };

            entity.Identity().Should().Be($"{param1.ToShortDateString()}-{param2}");
        }
    }
}
