using a3innuva.TAA.Migration.SDK.Interfaces;
using System;
using System.Collections.Generic;

namespace a3innuva.TAA.Migration.SDK.Implementations
{
    public class JournalLine : IJournalLine
    {
        public string Number { get; set; }
        public string Document { get; set; }
        public string Concept { get; set; }
        public string Account { get; set; }
        public string AccountDescription { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public Guid Id { get; set; }
        public int Line { get; set; }
        public string Identity()
        {
            return $"{this.Number}";
        }
        /// <summary>
        /// Analytic distributions
        /// </summary>
        public IEnumerable<IAnalyticDistribution> AnalyticDistributions { get; set; }
    }
}
