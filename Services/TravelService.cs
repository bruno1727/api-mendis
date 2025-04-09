using ApiMendis.Controllers.Requests;
using ApiMendis.DTOs.Responses;
using ApiMendis.Extensions;
using ApiMendis.OpenAI;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace ApiMendis.Services
{
    public class TravelService : ITravelService
    {
        private readonly IChatCompletionService _chatCompletionService;
        private readonly JsonSerializerOptions _serializerOptions;
        private readonly IDistributedCache _cache;
        private readonly ILogger<TravelService> _logger;

        public TravelService(IChatCompletionService chatCompletionService,
            JsonSerializerOptions serializerOptions,
            IDistributedCache cache,
            ILogger<TravelService> logger)
        {
            _chatCompletionService = chatCompletionService;
            _serializerOptions = serializerOptions;
            _cache = cache;
            _logger = logger;
        }

        public async Task<GetTravelResponse> GetAsync(GetTravelRequest request)
        {
            var message = $"3 destinos para viajar de férias, palavras-chave: {string.Join(", ", request.Characteristics)}. Responda em JSON com um array composto pelos campos: cidade, regiao, caracteristicas";
            var key = request.GetKeyCache();
            var result = await _cache.TryGetCacheAsnc<string>(key, _logger)
                ?? (await _chatCompletionService.GetAsync(message)).TryCache(_cache, key, _logger);

            var destinos = JsonSerializer.Deserialize<IEnumerable<GetTravelResponse.Destino>>(result!, _serializerOptions)!;

            return new GetTravelResponse(destinos);
        }
    }
}
