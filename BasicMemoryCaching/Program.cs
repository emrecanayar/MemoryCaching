using BasicMemoryCaching.Services;
using Microsoft.Extensions.DependencyInjection;


// Dependency Injection (DI) container'ı oluştur
var serviceProvider = new ServiceCollection()
    .AddMemoryCache()
    .AddSingleton<IProductService, ProductService>()
    .BuildServiceProvider();

// IProductService'yi DI container'dan al
var productService = serviceProvider.GetService<IProductService>();

// Ürün ID'sine göre ürünü al ve bilgileri yazdır
Console.WriteLine("Ürün ID'sini girin:");
var enteredProductId = Console.ReadLine();

if (int.TryParse(enteredProductId, out int productId))
{
    var product = productService.GetProductById(productId);
    if (product != null)
    {
        Console.WriteLine($"Ürün Adı: {product.Name}, Fiyat: {product.Price}");
    }
    else
    {
        Console.WriteLine("Ürün bulunamadı.");
    }
}
else
{
    Console.WriteLine("Geçersiz ID.");
}