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

            var connectionString = _configuration.GetConnectionString("Redis");
            
            if (string.IsNullOrEmpty(connectionString))
            {
                _logger.LogError("Redis connection string not found in configuration");
                return;
            }

            _logger.LogInformation("Connecting to Redis...");

            try
            {
                _redis = ConnectionMultiplexer.Connect(connectionString);
                _logger.LogInformation("Successfully connected to Redis");
            }
            catch (RedisConnectionException e)
            {
                _logger.LogWarning(e, "Falha ao tentar conectar no redis");
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
