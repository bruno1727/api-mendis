using StackExchange.Redis;

namespace ApiMendis.Services
{
    public class CacheService : ICacheService
    {
        private readonly ConnectionMultiplexer _redis;
        public CacheService(ConnectionMultiplexer redis)
        {
            _redis = redis;
        }

        public void FlushAll()
        {
            var endpoint = _redis.GetEndPoints().First();
            var server = _redis.GetServer(endpoint);
            server.FlushAllDatabases();
        }
    }
}
