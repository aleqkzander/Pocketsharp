using System.Text.Json.Serialization;

namespace Pocketsharp.PocketsharpObjects
{
    public class CollectionResponse<T>
    {
        [JsonPropertyName("items")]
        public List<T>? Items { get; set; }
    }
}
