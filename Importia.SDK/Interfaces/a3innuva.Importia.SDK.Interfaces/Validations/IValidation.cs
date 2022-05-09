namespace a3innuva.TAA.Migration.SDK.Interfaces
{
    using System;
    using System.Collections.Generic;

    public interface IValidation<in TEntity> : IDisposable where TEntity : IMigrationEntity
    {
        IEnumerable<IValidationResult> Validate(TEntity entity);
    }
}
