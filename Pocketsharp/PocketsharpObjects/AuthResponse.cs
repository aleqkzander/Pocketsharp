using System.Text.Json.Serialization;

namespace PocketsharpObjects
{
    public class AuthResponse
    {
        [JsonPropertyName("token")]
        public string? Token { get; private set; }

        [JsonPropertyName("record")]
        public AuthRecord? Record { get; set; }
    }
}
