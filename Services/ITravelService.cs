using api_mendis.DTOs.Responses;
using MendisWannaTravel.Controllers.Requests;

namespace MendisWannaTravel.Services
{
    public interface ITravelService
    {
        Task<GetTravelResponse> GetAsync(GetTravelRequest request);
    }
}