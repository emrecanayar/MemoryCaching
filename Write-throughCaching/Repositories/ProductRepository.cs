using Write_throughCaching.Entities;

namespace Write_throughCaching.Repositories
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

        public void UpdateProductDetailsInDatabase(Product product)
        {
            // Eğer ürün mevcutsa, detaylarını güncelle. Mevcut değilse, yeni ekle.
            var productDetail = Products.Find(p => p.Id == product.Id);
            if (productDetail is not null)
            {
                //Mevcut ürünü güncelle
                int index = Products.IndexOf(productDetail);
                productDetail.Name = product.Name;
                productDetail.Price = product.Price;
                Products[index] = productDetail;
            }
            else
            {
                Products.Add(new() { Id = product.Id, Name = product.Name, Price = product.Price }); // Yeni ürün ekle
            }
        }
    }
}
