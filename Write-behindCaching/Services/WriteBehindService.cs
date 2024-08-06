using Write_behindCaching.Entities;

namespace Write_behindCaching.Services
{
    public class WriteBehindService : IWriteBehindService
    {
        public void WriteBehind(Product product)
        {
            // Asenkron yazma işlemi burada yapılır
            Task.Run(() => SimulateDatabaseWrite(product));
        }

        private void SimulateDatabaseWrite(Product product)
        {
            // Veriyi veritabanına yazma işlemini simüle ediyoruz
            Thread.Sleep(2000); // Simüle edilen gecikme
            Console.WriteLine($"Product written to database: {product.Name}");
        }
    }
}
