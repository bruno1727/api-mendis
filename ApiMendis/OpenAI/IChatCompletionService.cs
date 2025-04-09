namespace ApiMendis.OpenAI
{
    public interface IChatCompletionService
    {
        Task<string> GetAsync(string message);
    }
}