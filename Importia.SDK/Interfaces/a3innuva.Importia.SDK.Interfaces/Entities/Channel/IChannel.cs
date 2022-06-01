namespace a3innuva.TAA.Migration.SDK.Interfaces
{
    public interface IChannel : IMigrationEntity, IMigrationSourceInfo
    {
        string Description { get; set; }
        string CorrelationId { get; set; }
        string ShortDescription { get; set; }
    }
}