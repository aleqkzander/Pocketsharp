using System.Text.Json.Serialization;

namespace Pocketsharp.Pocketsharp_Utilitys.Objects
{
    internal class AuthResponse
    {
        [JsonPropertyName("token")]
        public string? Token { get; set; }

        [JsonPropertyName("record")]
        public AuthRecord? Record { get; set; }
    }
}
