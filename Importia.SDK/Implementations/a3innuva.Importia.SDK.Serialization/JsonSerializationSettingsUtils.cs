namespace a3innuva.TAA.Migration.SDK.Serialization
{
    using Newtonsoft.Json;

    public static class JsonSerializationSettingsUtils
    {
        public static JsonSerializerSettings GetSettings(Formatting type = Formatting.None)
        {
            JsonSerializerSettings jsonSettings = new JsonSerializerSettings()
            {
                Formatting = type,
                NullValueHandling = NullValueHandling.Ignore,
                TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple,
                TypeNameHandling = TypeNameHandling.All,
                DateParseHandling = DateParseHandling.DateTime,
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                SerializationBinder = new a3innuvaSerializationBinder()
            };

            return jsonSettings;
        }
    }
}
