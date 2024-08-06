using Write_behindCaching.Entities;

namespace Write_behindCaching.Services
{
    public interface IWriteBehindService
    {
        void WriteBehind(Product product);
    }
}
