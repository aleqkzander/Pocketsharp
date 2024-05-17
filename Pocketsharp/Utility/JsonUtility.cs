using Pocketsharp.Objects;
using System.Text.Json;

namespace Pocketsharp.Utility
{
    public class JsonUtility
    {
        private static readonly JsonSerializerOptions serializerOptions = new() { WriteIndented = true };

        public static string SerializeRecordToJson(Record? record)
        {
            return JsonSerializer.Serialize(record, serializerOptions);
        }

        public static Record? DeserializeJsonToRecord(string json)
        {
            return JsonSerializer.Deserialize<Record?>(json);
        }

        public static string SerializeResponseToJson(Response? response)
        {
            return JsonSerializer.Serialize(response, serializerOptions);
        }

        public static Response? DeserializeJsonToResponse(string json)
        {
            return JsonSerializer.Deserialize<Response?>(json);
        }
    }
}