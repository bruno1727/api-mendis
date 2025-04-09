using ApiMendis.Services;
using AsyncAwaitBestPractices;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System.Text.Json;

namespace ApiMendis.Extensions
{
    public static class CacheExtensions
    {
        public static void AddCache(this IServiceCollection services, IConfiguration config)
        {
            services.AddSingleton<ICacheService, CacheService>();

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = config.GetConnectionString("Redis");
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
            catch (RedisConnectionException e)
            {
                logger.LogWarning(e, "Falha ao tenta conectar no redis");
                return default;
            }
            catch (Exception e)
            {
                logger.LogWarning(e, $"Falha em busar o cache da chave {key}");
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
            ).SafeFireAndForget(e => logger.LogWarning($"Falha em fazer o cahe da chave {key} erro {e}"));
        }

        public static T? TryCache<T>(this T? value, IDistributedCache cache, string key, ILogger logger, TimeSpan? tempoExpiracao = null)
        {
            cache.TryCache(key, value, logger, tempoExpiracao);
            return value;
        }
    }
}
