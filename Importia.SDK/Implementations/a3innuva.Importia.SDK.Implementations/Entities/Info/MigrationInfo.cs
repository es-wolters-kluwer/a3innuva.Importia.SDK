namespace a3innuva.TAA.Migration.SDK.Implementations
{
    using Interfaces;

    public class MigrationInfo : IMigrationInfo
    {       
        public string ActivityId { get; set; }
        public string ChannelId { get; set; }
        public string VatNumber { get; set; }
        public int Year { get; set; }
        public MigrationType Type { get; set; }
        public MigrationOrigin Origin { get; set; }
        public string FileName { get; set; }
        public string Version { get; set; }
 
    }
}
