using System.IO;
using System.Reflection;
using Newtonsoft.Json;

/*
Idea here is to use generics to return a datacontract model
*/
namespace Platform.Client.Mocks
{
    public class Utils
    {
        public static T ReadJsonFileAndDeserialize<T>(string fileName)
        {
            T model;
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Full
            };
            var resourcepath = "Platform.Client.Mocks.json." + fileName;
            using (var stream = GetResourceStream(resourcepath))
            using (var reader = new StreamReader(stream))
            {
                var jsonString = reader.ReadToEnd();
                model = JsonConvert.DeserializeObject<T>(jsonString, settings);
            }

            return model;
        }

        public static Stream GetResourceStream(string resource)
        {
            var assembly = Assembly.GetCallingAssembly();

            var stream = assembly.GetManifestResourceStream(resource);

            if (stream != null) return stream;
            assembly = Assembly.GetEntryAssembly();

            if (assembly == null)
            {
                return null;
            }

            stream = assembly.GetManifestResourceStream(resource);

            return stream;
        }
    }
}
