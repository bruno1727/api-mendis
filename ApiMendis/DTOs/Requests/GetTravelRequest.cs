namespace ApiMendis.Controllers.Requests
{
    public interface ICacheable
    {
        string GetKeyCache();
    }

    public record GetTravelRequest (IEnumerable<string> Characteristics) : ICacheable
    {
        public string GetKeyCache()
            => string.Join("-", Characteristics);
    }
}
