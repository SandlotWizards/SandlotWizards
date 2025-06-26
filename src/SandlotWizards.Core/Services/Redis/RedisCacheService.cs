using Microsoft.Extensions.Caching.Distributed;
using SandlotWizards.Core.Interfaces.Redis;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace SandlotWizards.Core.Services.Redis
{
    /// <summary>
    /// Provides Redis caching services using <see cref="IDistributedCache"/> as the backend.
    /// </summary>
    public class RedisCacheService : IRedisCacheService
    {
        private readonly IDistributedCache _cache;
        private static readonly TimeSpan DefaultTTL = TimeSpan.FromHours(6);

        /// <summary>
        /// Initializes a new instance of the <see cref="RedisCacheService"/>.
        /// </summary>
        /// <param name="cache">The distributed cache instance.</param>
        public RedisCacheService(IDistributedCache cache)
        {
            _cache = cache;
        }

        /// <inheritdoc />
        public async Task SetAsync<T>(string key, T value, TimeSpan? ttl = null, CancellationToken cancellationToken = default)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = ttl ?? DefaultTTL
            };

            var json = JsonSerializer.Serialize(value, new JsonSerializerOptions
            {
                WriteIndented = false
            });

            await _cache.SetStringAsync(key, json, options, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
        {
            var json = await _cache.GetStringAsync(key, cancellationToken);
            if (string.IsNullOrEmpty(json))
                return default;

            return JsonSerializer.Deserialize<T>(json);
        }

        /// <inheritdoc />
        public async Task<bool> ExistsAsync(string key, CancellationToken cancellationToken = default)
        {
            var value = await _cache.GetStringAsync(key, cancellationToken);
            return value != null;
        }

        /// <inheritdoc />
        public async Task DeleteAsync(string key, CancellationToken cancellationToken = default)
        {
            await _cache.RemoveAsync(key, cancellationToken);
        }
    }
}
