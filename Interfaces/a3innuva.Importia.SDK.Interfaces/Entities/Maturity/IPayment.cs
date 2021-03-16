namespace a3innuva.TAA.Migration.SDK.Interfaces
{
    public interface IPayment : IMaturity
    {
        PaymentSituation Situation { get; set; }
    }
}
