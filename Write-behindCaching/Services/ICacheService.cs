using Write_behindCaching.Entities;

namespace Write_behindCaching.Services
{
    public interface ICacheService
    {
        void AddOrUpdateProduct(Product product);
        Product GetProduct(int id);
    }
}
