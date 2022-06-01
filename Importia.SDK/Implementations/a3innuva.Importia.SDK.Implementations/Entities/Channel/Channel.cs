using System;
using a3innuva.TAA.Migration.SDK.Interfaces;

namespace a3innuva.TAA.Migration.SDK.Implementations
{
    public class Channel : IChannel
    {
        public string Identity() => Id.ToString();
        public string Description { get; set; }
        public string CorrelationId { get; set; }
        public string ShortDescription { get; set; }
        public Guid Id { get; set; }
        public int Line { get; set; }

        public string Source { get; set; }
    }
}