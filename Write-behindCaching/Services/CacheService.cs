using Microsoft.Extensions.Caching.Memory;
using Write_behindCaching.Entities;

namespace Write_behindCaching.Services
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _cache;
        private readonly IWriteBehindService _writeBehindService;

        public CacheService(IMemoryCache cache, IWriteBehindService writeBehindService)
        {
            _cache = cache;
            _writeBehindService = writeBehindService;
        }

        public void AddOrUpdateProduct(Product product)
        {
            _cache.Set(product.Id, product);
            _writeBehindService.WriteBehind(product);
        }

        public Product GetProduct(int id)
        {
            _cache.TryGetValue(id, out Product product);
            return product;
        }
    }
}
