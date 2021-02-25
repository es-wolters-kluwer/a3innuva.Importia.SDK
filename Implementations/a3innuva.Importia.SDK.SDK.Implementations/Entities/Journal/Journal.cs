namespace a3innuva.TAA.Migration.SDK.Implementations
{
    using System;
    using a3innuva.TAA.Migration.SDK.Interfaces;

    public class Journal : IJournal
    {
        public DateTime Date { get; set; }
        public string Number { get; set; }
        public string Document { get; set; }
        public IJournalLine[] Lines { get; set; }
        public JournalTypes Type { get; set; }
        public Guid Id { get; set; }
        public int Line { get; set; }
        public string Identity()
        {
            return $"{this.Date.ToShortDateString()}-{this.Number}";
        }
        public string Source { get; set; }
    }
}
