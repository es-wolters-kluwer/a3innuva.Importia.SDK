namespace a3innuva.TAA.Migration.SDK.Interfaces
{
    public interface ICharge : IMaturity
    {
        ChargeSituation Situation { get; set; }
    }
}