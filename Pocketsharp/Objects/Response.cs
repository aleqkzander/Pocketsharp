using System.Text.Json.Serialization;

namespace Pocketsharp.Objects
{
    public class Response
    {
        [JsonPropertyName("token")]
        public string? Token { get; set; } = string.Empty;

        [JsonPropertyName("record")]
        public Record? Record { get; set; } = new();
    }
}