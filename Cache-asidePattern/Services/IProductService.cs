using Cache_asidePattern.Entities;

namespace Cache_asidePattern.Services
{
    public interface IProductService
    {
        Product GetProductDetails(int productId);
        void UpdateProductDetails(Product product);
    }
}
