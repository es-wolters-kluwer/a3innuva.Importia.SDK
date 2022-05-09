namespace a3innuva.TAA.Migration.SDK.Implementations
{
    using Interfaces;

    public class MigrationSet : IMigrationSet
    {
        public IMigrationInfo Info { get; set; }
        public IMigrationEntity[] Entities { get; set; }
    }
}
