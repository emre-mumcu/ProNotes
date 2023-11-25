using Newtonsoft.Json;
using System.Globalization;

namespace ProNotes.AppLib.Serialization
{
    public class JsonSerialization
    {
        private JsonSerializerSettings jsonSerializerSettings
        {
            get
            {
                return new JsonSerializerSettings()
                {
                    Formatting = Formatting.None,
                    PreserveReferencesHandling = PreserveReferencesHandling.None,
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    NullValueHandling = NullValueHandling.Include,
                    Culture = new CultureInfo("tr-TR")
                };
            }
        }

        public string? Serialize<T>(T obj, Formatting formatting = Formatting.None)
        {
            if (EqualityComparer<T>.Default.Equals(obj, default))
                return null;

            return JsonConvert.SerializeObject(obj, formatting, jsonSerializerSettings);
        }

        public T? Deserialize<T>(string obj)
        {
            return JsonConvert.DeserializeObject<T>(obj, jsonSerializerSettings);
        }
    }
}

/*

Serializing and Deserializing JSON

JsonConvert
For simple scenarios where you want to convert to and from a JSON string, the SerializeObject() and DeserializeObject() methods on JsonConvert provide an easy-to-use wrapper over JsonSerializer.

JsonSerializer
For more control over how an object is serialized, the JsonSerializer can be used directly. The JsonSerializer is able to read and write JSON text directly to a stream via JsonTextReader and JsonTextWriter. Other kinds of JsonWriters can also be used, such as JTokenReader/JTokenWriter, to convert your object to and from LINQ to JSON objects, or BsonReader/BsonWriter, to convert to and from BSON.

*/

//public class JsonSerialization
//{
//    private JsonSerializerSettings jsonSerializerSettings => new JsonSerializerSettings
//    {
//        Formatting = Formatting.None,
//        PreserveReferencesHandling = PreserveReferencesHandling.None,
//        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
//        NullValueHandling = NullValueHandling.Include,
//        Culture = new CultureInfo("tr-TR")
//    };

//    public string? Serialize<T>(T obj, Formatting formatting = Formatting.None)
//    {
//        if (EqualityComparer<T>.Default.Equals(obj, default))
//        {
//            return null;
//        }

//        return JsonConvert.SerializeObject(obj, formatting, jsonSerializerSettings);
//    }

//    public T? Deserialize<T>(string obj)
//    {
//        return JsonConvert.DeserializeObject<T>(obj, jsonSerializerSettings);
//    }
//}