using System.Text.Json.Serialization;

namespace Pocketsharp.Objects
{
    public class AuthResponse
    {
        [JsonPropertyName("token")]
        public string? Token { get; set; }

        [JsonPropertyName("record")]
        public AuthRecord? Record { get; set; }
    }
}
