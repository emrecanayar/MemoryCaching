using Microsoft.Extensions.Caching.Memory;

namespace SlidingExpiration.Services
{
    public class DataService
    {
        private readonly IMemoryCache _memoryCache;

        public DataService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public string GetDate(string key)
        {
            if (!_memoryCache.TryGetValue(key, out string cachedData))
            {
                // Öğe önbellekte yoksa, veriyi alın ve önbelleğe ekleyin
                cachedData = $"Data for {key} - {DateTime.Now}";

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(5)) // 5 dakikalık kaydırmalı sona erme süresi
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(10)) // 1 saatlik mutlak sona erme süresi
                    .SetPriority(CacheItemPriority.Normal)  // Normal öncelikli önbellek öğesi
                    .SetSize(1); ; // Boyutunu belirleyin (MemoryCache'in boyut sınırı varsa kullanışlı)

                _memoryCache.Set(key, cachedData, cacheEntryOptions);
            }

            return cachedData;
        }
    }
}


