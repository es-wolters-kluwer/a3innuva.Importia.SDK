namespace a3innuva.TAA.Migration.SDK.Interfaces
{
    using System.Collections.Generic;

    public interface IMigrationSet
    {
        IMigrationInfo Info { get; set; }
        IMigrationEntity[] Entities { get; set; }
    }
}
