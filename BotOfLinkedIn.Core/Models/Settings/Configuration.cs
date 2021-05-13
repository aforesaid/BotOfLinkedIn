using System.Text.Json.Serialization;

namespace BotOfLinkedIn.Core.Models.Settings
{
    public class Configuration
    {
        [JsonPropertyName("Cookie")]
        public string Cookie { get; set; }
        [JsonPropertyName("CsrfToken")]
        public string CsrfToken { get; set; }
    }
}