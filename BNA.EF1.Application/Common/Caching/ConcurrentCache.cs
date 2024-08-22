using System.Collections.Concurrent;

namespace BNA.EF1.Application.Common.Caching
{
    public sealed class ConcurrentCache<T> where T : class
    {
        public ConcurrentDictionary<string, Cache<T>> CachedValues { get; private set; } = new ConcurrentDictionary<string, Cache<T>>();

        public void Add(string key, Cache<T> value)
        {
            CachedValues.TryAdd(key, value);
        }

        public Cache<T>? GetCachedValue(string key)
        {
            Cache<T>? value;

            if (CachedValues.TryGetValue(key, out value))
                return value;

            return null;
        }

        public void ValidateCachedValues()
        {
            var values = CachedValues.Where(x => x.Value.IsExpired()).ToList();

            foreach (var value in values)
                CachedValues.TryRemove(value.Key, out Cache<T>? obj);

        }
    }
}
