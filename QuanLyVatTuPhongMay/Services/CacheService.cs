using QuanLyVatTuPhongMay.Services.Interfaces;

namespace QuanLyVatTuPhongMay.Services
{
    public class CacheService : ICacheService
    {
        private readonly Dictionary<string, (object value, DateTime expiration)> _cache = new();

        public async Task<T?> GetAsync<T>(string key) where T : class
        {
            if (_cache.TryGetValue(key, out var cached))
            {
                if (cached.expiration > DateTime.Now)
                {
                    return cached.value as T;
                }
                else
                {
                    _cache.Remove(key);
                }
            }
            return null;
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan? expiration = null) where T : class
        {
            var expirationTime = DateTime.Now.Add(expiration ?? TimeSpan.FromHours(1));
            _cache[key] = (value, expirationTime);
        }

        public async Task RemoveAsync(string key)
        {
            _cache.Remove(key);
        }

        public async Task RemovePatternAsync(string pattern)
        {
            var keysToRemove = _cache.Keys.Where(k => k.Contains(pattern)).ToList();
            foreach (var key in keysToRemove)
            {
                _cache.Remove(key);
            }
        }

        public async Task<bool> ExistsAsync(string key)
        {
            return _cache.ContainsKey(key) && _cache[key].expiration > DateTime.Now;
        }
    }
} 