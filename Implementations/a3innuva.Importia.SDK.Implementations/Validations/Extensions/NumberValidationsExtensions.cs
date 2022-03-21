namespace a3innuva.TAA.Migration.SDK.Implementations
{
    public static class NumberValidationsExtensions
    {
        public static bool ValidatePercentage(this decimal ? input)
        {
            if (input == null)
                return true;

            return 0 <= input &&  input <= 100;
        }
    }
}
