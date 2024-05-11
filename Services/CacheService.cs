using StackExchange.Redis;

namespace ApiMendis.Services
{
    public class CacheService : ICacheService
    {
        private ConnectionMultiplexer? _redis;
        private readonly ConfigurationOptions _configOptions;
        private readonly ILogger<CacheService> _logger;
        public CacheService(
            ConfigurationOptions configOptions,
            ILogger<CacheService> logger)
        {
            _configOptions = configOptions;
            _logger = logger;
        }

        private void Connect()
        {
            if (_redis != null) return;

            try
            {
                _redis = ConnectionMultiplexer.Connect(_configOptions);
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
