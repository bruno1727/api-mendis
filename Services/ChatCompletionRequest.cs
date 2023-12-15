using System.Text.Json.Serialization;
using static MendisWannaTravel.Services.ChatCompletionRequest;

namespace MendisWannaTravel.Services
{
    public record ChatCompletionRequest(
        IEnumerable<MessageRequest> Messages,
        string Model = "gpt-3.5-turbo"
        )
    {

        public record MessageRequest(string Content, string Role = "user", string? Name = null);
    };
    
}
