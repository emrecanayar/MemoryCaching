using Cache_asidePattern.Entities;
using Cache_asidePattern.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace Cache_asidePattern.Services
{
    public class ProductService : IProductService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IProductRepository _productRepository;

        public ProductService(IMemoryCache memoryCache, IProductRepository productRepository)
        {
            _memoryCache = memoryCache;
            _productRepository = productRepository;
        }

        public Product GetProductDetails(int productId)
        {
            //Cache anahtarı oluşturuluyor
            string cacheKey = $"Product_{productId}";

            //Cache'te veri olup olmadığı kontrol ediliyor.
            if (!_memoryCache.TryGetValue(cacheKey, out Product product))
            {
                //Veri cachte'te yoksa, veriyi veritabanından al ve cache ekle.
                product = _productRepository.GetProductDetailsFromDatabase(productId);

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(10)); // 10 dakika sonra cache'ten silinecek.

                _memoryCache.Set(cacheKey, product, cacheEntryOptions);
            }

            //Cache de veri varsa direkt cache'ten döndür.
            return product;
        }

        public void UpdateProductDetails(Product product)
        {
            // Veritabanında güncelleme işlemini simüle edelim.
            Product? productDetail = _productRepository.Products.Find(p => p.Id == product.Id);
            if (productDetail != null)
            {
                int index = _productRepository.Products.IndexOf(productDetail);
                productDetail.Name = product.Name;
                productDetail.Price = product.Price;
                _productRepository.Products[index] = productDetail;
                // Cache'i güncelle veya temizle
                string cacheKey = $"Product_{product.Id}";
                _memoryCache.Remove(cacheKey); // Cache'teki eski veriyi kaldırıyoruz
            }

        }
    }
}
