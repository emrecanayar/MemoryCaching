using Write_throughCaching.Entities;

namespace Write_throughCaching.Repositories
{
    public interface IProductRepository
    {
        List<Product> Products { get; set; }
        Product GetProductDetailsFromDatabase(int productId);
        void UpdateProductDetailsInDatabase(Product product);
    }
}
