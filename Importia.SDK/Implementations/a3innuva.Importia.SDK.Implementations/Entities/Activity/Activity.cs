using System;
using a3innuva.TAA.Migration.SDK.Interfaces;

namespace a3innuva.TAA.Migration.SDK.Implementations
{
    public class Activity : IActivity
    {
        public Guid Id { get; set; }
        public int Line { get; set; }

        public string Source { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public Taxation Taxation { get; set; }
        public IEstimation[] Estimations { get; set; }
        public string Identity()
        {
            return $"{Id}";
        }
    }
}