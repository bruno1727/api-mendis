namespace ApiMendis.Services
{
    public record ChatCompletionResponse(IEnumerable<ChoiceResponse> Choices);
    public record ChoiceResponse(MessageResponse Message);
    public record MessageResponse(string Role, string Content);
}