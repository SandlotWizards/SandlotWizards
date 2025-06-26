using System;
using System.Threading;
using System.Threading.Tasks;

namespace SandlotWizards.Core.Interfaces.Redis
{
    /// <summary>
    /// Defines a service for interacting with Redis as a distributed cache.
    /// </summary>
    public interface IRedisCacheService
    {
        /// <summary>
        /// Stores a value in Redis with an optional time-to-live (TTL).
        /// </summary>
        /// <typeparam name="T">The type of the object to store.</typeparam>
        /// <param name="key">The Redis key under which the value is stored.</param>
        /// <param name="value">The object to store.</param>
        /// <param name="ttl">Optional TTL for the cache entry.</param>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task SetAsync<T>(string key, T value, TimeSpan? ttl = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves a value from Redis by its key.
        /// </summary>
        /// <typeparam name="T">The expected type of the object to retrieve.</typeparam>
        /// <param name="key">The Redis key to retrieve.</param>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        /// <returns>The retrieved object, or null if not found.</returns>
        Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default);

        /// <summary>
        /// Determines whether a key exists in Redis.
        /// </summary>
        /// <param name="key">The Redis key to check.</param>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        /// <returns>True if the key exists; otherwise, false.</returns>
        Task<bool> ExistsAsync(string key, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes a key from Redis.
        /// </summary>
        /// <param name="key">The Redis key to delete.</param>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task DeleteAsync(string key, CancellationToken cancellationToken = default);

        /// <summary>
        /// Overload that calls <see cref="SetAsync{T}(string, T, TimeSpan?, CancellationToken)"/> with no TTL.
        /// </summary>
        /// <typeparam name="T">The type of the object to store.</typeparam>
        /// <param name="key">The Redis key.</param>
        /// <param name="value">The object to store.</param>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task SetAsync<T>(string key, T value, CancellationToken cancellationToken)
            => SetAsync(key, value, null, cancellationToken);
    }
}
