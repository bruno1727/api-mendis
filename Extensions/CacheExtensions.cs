using ApiMendis.Services;
using AsyncAwaitBestPractices;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using System.Text.Json;

namespace ApiMendis.Extensions
{
    public static class CacheExtensions
    {
        public static void AddCache(this IServiceCollection services, IConfiguration config)
        {
            var configOptions = new ConfigurationOptions()
            {
                EndPoints = {
                    { config["Redis:EndPoint:Host"]!, int.Parse(config["Redis:EndPoint:Port"]!) },
                },
                User = config["Redis:User"]!,
                Password = config["Redis:Password"],
                AllowAdmin = true
            };

            var redis = ConnectionMultiplexer.Connect(configOptions);
            services.AddSingleton<ICacheService>((_) => new CacheService(redis));

            services.AddStackExchangeRedisCache(options =>
            {
                options.InstanceName = "redis-RV7c";
                options.ConfigurationOptions = configOptions;
            });
        }

        public async static Task<T?> TryGetCacheAsnc<T>(this IDistributedCache cache, string key, ILogger logger)
        {
            try
            {
                var resultCached = await cache.GetStringAsync(key);
                if (!string.IsNullOrEmpty(resultCached))
                {
                    logger.LogDebug($"Retornando valor de cache da chave {key}");
                    return JsonSerializer.Deserialize<T>(resultCached);
                }

                return default;
            }
            catch (Exception e)
            {
                logger.LogError(e, $"Falha em busar o cache da chave {key}");
                return default;
            }
        }

        public static void TryCache<T>(this IDistributedCache cache, string key, T? value, ILogger logger, TimeSpan? tempoExpiracao = null)
        {
            if (value == null) return;
            cache.SetStringAsync(
                key,
                JsonSerializer.Serialize(value),
                new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = tempoExpiracao ?? TimeSpan.FromHours(1) }
            ).SafeFireAndForget(e => logger.LogError($"Falha em fazer o cahe da chave {key} erro {e}"));
        }

        public static T? TryCache<T>(this T? value, IDistributedCache cache, string key, ILogger logger, TimeSpan? tempoExpiracao = null)
        {
            cache.TryCache(key, value, logger, tempoExpiracao);
            return value;
        }
    }
}
