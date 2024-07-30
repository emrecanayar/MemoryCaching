using Cache_asidePattern.Entities;

namespace Cache_asidePattern.Repositories
{
    public interface IProductRepository
    {
        List<Product> Products { get; set; }
        Product GetProductDetailsFromDatabase(int productId);
    }
}
