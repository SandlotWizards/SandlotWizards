using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace SandlotWizards.AiPipelines.Helpers
{
    public static class YamlSerializer
    {
        private static readonly ISerializer _serializer = new SerializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();

        private static readonly IDeserializer _deserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();

        public static string Serialize<T>(T obj)
        {
            return _serializer.Serialize(obj);
        }

        public static T Deserialize<T>(string yaml)
        {
            return _deserializer.Deserialize<T>(yaml);
        }
    }
}
