using ApiMendis.DTOs.Responses;
using ApiMendis.Controllers.Requests;

namespace ApiMendis.Services
{
    public interface ISuggestionService
    {
        Task<GetSuggestionsResponse> GetAsync(GetSuggestionsRequest request);
    }
}