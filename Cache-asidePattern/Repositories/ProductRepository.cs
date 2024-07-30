using Cache_asidePattern.Entities;

namespace Cache_asidePattern.Repositories
{
    public class ProductRepository : IProductRepository
    {
        // Bu örnekte, veritabanı yerine bir in-memory veri kaynağı kullanıyoruz
        private List<Product> _products = new List<Product>()
        {
            new(){ Id = 1, Name = "Product 1", Price = 100 },
            new(){ Id = 2, Name = "Product 2", Price = 200 },
            new(){ Id = 3, Name = "Product 3", Price = 300 },
            new(){ Id = 4, Name = "Product 4", Price = 400 },
            new(){ Id = 5, Name = "Product 5", Price = 500 }
        };

        public List<Product> Products
        {
            get { return _products; }
            set { _products = value; }
        }
        public Product GetProductDetailsFromDatabase(int productId)
        {
            // Veri tabanından veri alınıyor gibi simüle edelim.
            var product = _products.FirstOrDefault(p => p.Id == productId);
            return product;
        }
    }
}



