namespace a3innuva.TAA.Migration.SDK.Interfaces
{
    public interface IValidationResult
    {
        int Line { get; set; }
        string Code { get; set; }
        bool IsValid { get; set; }
    }
}
