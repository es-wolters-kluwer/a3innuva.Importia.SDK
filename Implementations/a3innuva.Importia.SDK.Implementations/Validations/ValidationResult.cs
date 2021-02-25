namespace a3innuva.TAA.Migration.SDK.Implementations
{
    using a3innuva.TAA.Migration.SDK.Interfaces;

    public class ValidationResult : IValidationResult
    {
        public int Line { get; set; }
        public string Code { get; set; }
        public bool IsValid { get; set; }
    }
}
