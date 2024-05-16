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
    }
}