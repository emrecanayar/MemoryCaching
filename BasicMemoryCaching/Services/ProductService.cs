using BasicMemoryCaching.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace BasicMemoryCaching.Services
{
    public class ProductService : IProductService
    {
        // Ürünleri tutan bir liste tanımlanıyor. Bu liste, veritabanı yerine kullanılan bir simülasyon.
        private readonly List<Product> _products = new List<Product>
        {
            new() { Id = 1, Name = "Product 1", Price = 10 },
            new() { Id = 2, Name = "Product 2", Price = 20 },
            new() { Id = 3, Name = "Product 3", Price = 30 },
            new() { Id = 4, Name = "Product 4", Price = 40 },
            new() { Id = 5, Name = "Product 5", Price = 50 } };

        // IMemoryCache örneği, bellek içi önbellekleme yapmak için kullanılır.
        private readonly IMemoryCache _memoryCache;

        public ProductService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        // Belirli bir ürün ID'sine göre ürünü döndüren bir metot.
        public Product GetProductById(int productId)
        {
            // Önbellekteki ürün için anahtar oluşturulur.
            string cacheKey = $"Product_{productId}";

            // Önbellekte ürün olup olmadığı kontrol edilir.
            if (!_memoryCache.TryGetValue(cacheKey, out Product product))
            {
                // Eğer ürün önbellekte değilse, listeden ürün çekilir (veritabanı yerine simülasyon).
                product = _products.FirstOrDefault(p => p.Id == productId);

                // Önbellek girişi için seçenekler belirlenir. Burada kayma süresi 30 dakika olarak ayarlanmıştır.
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(30));

                // Ürün önbelleğe eklenir.
                _memoryCache.Set(cacheKey, product, cacheEntryOptions);
            }

            return product;
        }
    }
}
