namespace ApiMendis.Controllers.Requests
{
    public interface ICacheable
    {
        string GetKeyCache();
    }

    public record GetTravelRequest (
        string Country) : ICacheable
    {
        public IEnumerable<string> Characteristics { get; set; } = new List<string>();

        public string GetKeyCache()
            => Country + string.Join("-", Characteristics);
    }
}
