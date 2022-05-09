namespace a3innuva.TAA.Migration.SDK.Serialization
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json.Serialization;

    public class a3innuvaSerializationBinder: ISerializationBinder
    {
        public IList<Type> KnownTypes => this.knownTypes;
        
        private readonly IList<Type> knownTypes = new List<Type>()
        {
            typeof(a3innuva.TAA.Migration.SDK.Implementations.Account),
            typeof(a3innuva.TAA.Migration.SDK.Implementations.InputInvoice),
            typeof(a3innuva.TAA.Migration.SDK.Implementations.InputInvoiceLine),
            typeof(a3innuva.TAA.Migration.SDK.Implementations.Journal),
            typeof(a3innuva.TAA.Migration.SDK.Implementations.JournalLine),
            typeof(a3innuva.TAA.Migration.SDK.Implementations.MigrationInfo),
            typeof(a3innuva.TAA.Migration.SDK.Implementations.MigrationSet),
            typeof(a3innuva.TAA.Migration.SDK.Implementations.OutputInvoice),
            typeof(a3innuva.TAA.Migration.SDK.Implementations.OutputInvoiceLine),
            typeof(a3innuva.TAA.Migration.SDK.Implementations.Payment),
            typeof(a3innuva.TAA.Migration.SDK.Implementations.Charge),
            typeof(a3innuva.TAA.Migration.SDK.Implementations.InputInvoiceAdditionalData),
            typeof(a3innuva.TAA.Migration.SDK.Implementations.OutputInvoiceAdditionalData),

            typeof(a3innuva.TAA.Migration.SDK.Interfaces.IMigrationEntity[]),
            typeof(a3innuva.TAA.Migration.SDK.Interfaces.IInputInvoiceLine[]),
            typeof(a3innuva.TAA.Migration.SDK.Interfaces.IJournalLine[]),
            typeof(a3innuva.TAA.Migration.SDK.Interfaces.IOutputInvoiceLine[]),
            typeof(a3innuva.TAA.Migration.SDK.Interfaces.IPayment[]),
            typeof(a3innuva.TAA.Migration.SDK.Interfaces.ICharge[])
        };

        public Type BindToType(string assemblyName, string typeName)
        {
            var type = this.knownTypes.SingleOrDefault(t => t.FullName == typeName);
            if (type == null)
                throw new ArgumentException($"{typeName} no es un tipo serializable valido");

            return type;
        }

        public void BindToName(Type serializedType, out string assemblyName, out string typeName)
        {
            assemblyName = null;
            typeName = serializedType.FullName;
        }
    }
}
