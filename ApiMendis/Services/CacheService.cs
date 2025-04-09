using StackExchange.Redis;

namespace ApiMendis.Services
{
    public class CacheService : ICacheService
    {
        private ConnectionMultiplexer? _redis;
        private IConfiguration _configuration;
        private readonly ILogger<CacheService> _logger;
        public CacheService(
            ILogger<CacheService> logger,
            IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        private void Connect()
        {
            if (_redis != null) return;

            try
            {
                _redis = ConnectionMultiplexer.Connect(_configuration.GetConnectionString("Redis"));
            }
            catch (RedisConnectionException e)
            {
                _logger.LogWarning(e, "Falha ao tenta conectar no redis");
            }
        }

        public void FlushAll()
        {
            Connect();
            if (_redis == null) return;
            var endpoint = _redis!.GetEndPoints().First();
            var server = _redis.GetServer(endpoint);
            server.FlushAllDatabases();
        }
    }
}
