using api_mendis.DTOs.Responses;
using api_mendis.OpenAI;
using MendisWannaTravel.Controllers.Requests;
using System.Text.Json;

namespace MendisWannaTravel.Services
{

    public class TravelService : ITravelService
    {
        private readonly IChatCompletionService _chatCompletionService;
        private readonly JsonSerializerOptions _serializerOptions;

        public TravelService(IChatCompletionService chatCompletionService,
            JsonSerializerOptions serializerOptions)
        {
            _chatCompletionService = chatCompletionService;
            _serializerOptions = serializerOptions;
        }

        public async Task<GetTravelResponse> GetAsync(GetTravelRequest request)
        {
            var message = $"3 destinos para viajar de férias, palavras-chave: {string.Join(", ", request.Characteristics)}. Responda em JSON com os campos: cidade, regiao, caracteristicas";
            var result = await _chatCompletionService.GetAsync(message);
            return JsonSerializer.Deserialize<GetTravelResponse>(result, _serializerOptions)!;
        }
    }
}
