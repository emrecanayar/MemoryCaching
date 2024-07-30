using Write_throughCaching.Entities;

namespace Write_throughCaching.Services
{
    public interface IProductService
    {
        Product GetProductDetails(int productId);
        void UpdateProductDetails(Product product);
    }
}
