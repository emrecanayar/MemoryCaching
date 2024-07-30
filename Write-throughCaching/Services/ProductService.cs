using Microsoft.Extensions.Caching.Memory;
using Write_throughCaching.Entities;
using Write_throughCaching.Repositories;

namespace Write_throughCaching.Services
{
    public class ProductService : IProductService
    {
        private readonly IMemoryCache _memoryCache; // Cache işlemi için IMemoryCache arayüzü.
        private readonly IProductRepository _productRepository; // Veritabanı erişimi için ProductRepository sınıfı.

        public ProductService(IMemoryCache memoryCache, IProductRepository productRepository)
        {
            _memoryCache = memoryCache;
            _productRepository = productRepository;
        }

        public Product GetProductDetails(int productId)
        {
            string cacheKey = $"Product_{productId}"; // Cache anahtarı oluşturuluyor.

            if (!_memoryCache.TryGetValue(cacheKey, out Product product)) // Cache'te veri yoksa
            {
                // Eğer önbellekte veri yoksa, veritabanından veriyi al.
                product = _productRepository.GetProductDetailsFromDatabase(productId);

                _memoryCache.Set(cacheKey, product, new MemoryCacheEntryOptions
                { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10) } // Ön bellekte saklanacak verinin süresi 10 dakika olarak ayarlanıyor.
                ); // Cache'e veri ekleniyor.
            }

            return product;
        }

        public void UpdateProductDetails(Product product)
        {
            // Veritabanını ve önbelleği güncelle (write-through caching).
            _productRepository.UpdateProductDetailsInDatabase(product);

            string cacheKey = $"Product_{product.Id}"; // Cache anahtarı oluşturuluyor.
            _memoryCache.Set(cacheKey, product, new MemoryCacheEntryOptions
            { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10) } // Ön bellekte saklanacak verinin süresi 10 dakika olarak ayarlanıyor.
            ); // Cache'e veri ekleniyor.

        }
    }
}
