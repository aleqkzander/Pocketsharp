using System.Text.Json.Serialization;

namespace Pocketsharp.Objects
{
    public class Response
    {
        [JsonPropertyName("token")]
        public string? Token { get; set; }

        [JsonPropertyName("record")]
        public Record? Record { get; set; }
    }
}