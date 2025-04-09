using ApiMendis.DTOs.Responses;
using ApiMendis.Controllers.Requests;

namespace ApiMendis.Services
{
    public interface ITravelService
    {
        Task<GetTravelResponse> GetAsync(GetTravelRequest request);
    }
}