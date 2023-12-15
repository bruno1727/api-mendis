namespace api_mendis.OpenAI
{
    public interface IChatCompletionService
    {
        Task<string> GetAsync(string message);
    }
}