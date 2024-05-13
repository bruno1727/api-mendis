using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace ApiMendis
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : IEntity
    {
        private readonly IDistributedCache _cache;

        public Repository(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<bool> InsertAsync(TEntity entity)
        {
            await _cache.SetStringAsync(RedisIdMapper.Map(entity), JsonSerializer.Serialize(entity));

            return true;
        }
    }
}
