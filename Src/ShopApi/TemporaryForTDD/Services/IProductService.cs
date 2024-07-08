using ShopApi.TemporaryForTDD.Models;

namespace ShopApi.TemporaryForTDD.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task<Product> CreateProductAsync(ProductDto productDto);
        Task<Product> UpdateProductAsync(int id, ProductDto productDto);
        Task DeleteProductAsync(int id);
    }
}
