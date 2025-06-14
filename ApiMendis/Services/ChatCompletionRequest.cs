using System.Text.Json.Serialization;
using static ApiMendis.Services.ChatCompletionRequest;

namespace ApiMendis.Services
{
    public record ChatCompletionRequest(
        IEnumerable<MessageRequest> Messages,
        string Model = "gpt-4.1"
        )
    {
        [JsonPropertyName("response_format")]
        public ResponseFormatRequest ResponseFormat { get; set; } = new ResponseFormatRequest("json_object");

        public record MessageRequest(string Content, string Role = "user", string? Name = null);

        public record ResponseFormatRequest(string Type);
    };
    
}
