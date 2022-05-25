namespace a3innuva.TAA.Migration.SDK.Interfaces
{
    public interface IActivity : IMigrationEntity, IMigrationSourceInfo
    {
        string Description { get; set; }
        string ShortDescription { get; set; }
        Taxation Taxation { get; set; }

        IEstimation[] Estimations { get; set; }
    }
}