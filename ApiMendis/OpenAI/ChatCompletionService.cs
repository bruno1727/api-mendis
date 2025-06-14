﻿using ApiMendis.Services;
using System.Text.Json;

namespace ApiMendis.OpenAI
{
    public class ChatCompletionService : IChatCompletionService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<ChatCompletionService> _logger;
        private readonly JsonSerializerOptions _serializerOptions;
        private readonly IConfiguration _configuration;

        public ChatCompletionService(IHttpClientFactory httpClientFactory,
            ILogger<ChatCompletionService> logger,
            JsonSerializerOptions serializerOptions,
            IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _serializerOptions = serializerOptions;
            _configuration = configuration;
        }

        public async Task<string> GetAsync(string message)
        {
            var request = new ChatCompletionRequest(
                new List<ChatCompletionRequest.MessageRequest>() { new(message.ToString()) }
            );

            var apiKey = _configuration["OpenAI:ApiKey"];
            using HttpClient client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("https://api.openai.com/v1/");
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
            _logger.LogInformation($"Request: {JsonSerializer.Serialize(request, _serializerOptions)}");
            using HttpResponseMessage response = await client.PostAsJsonAsync("chat/completions", request, _serializerOptions);

            _logger.LogInformation($"Response: {await response.Content.ReadAsStringAsync()}");

            response.EnsureSuccessStatusCode();

            var contentStream = await response.Content.ReadAsStreamAsync();

            var result = await JsonSerializer.DeserializeAsync
                <ChatCompletionResponse>(contentStream, _serializerOptions);

            return result?.Choices?.FirstOrDefault()?.Message?.Content
                .Replace("```json", "")
                .Replace("```", "") ?? "";
        }
    }
}
