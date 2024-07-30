using BasicMemoryCaching.Entities;

namespace BasicMemoryCaching.Services
{
    public interface IProductService
    {
        Product GetProductById(int productId);
    }
}
