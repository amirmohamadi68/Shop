using ShopApi.TemporaryForTDD.Models;

namespace ShopApi.TemporaryForTDD.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _productRepository.GetProductsAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _productRepository.GetProductByIdAsync(id);
        }

        public async Task<Product> CreateProductAsync(ProductDto productDto)
        {
            var product = new Product
            {
                Name = productDto.Name,
                CategoryId = productDto.CategoryId
            };

            return await _productRepository.CreateProductAsync(product);
        }

        public async Task<Product> UpdateProductAsync(int id, ProductDto productDto)
        {
            var existingProduct = await _productRepository.GetProductByIdAsync(id);

            if (existingProduct == null)
            {
                throw new Exception($"Product with id {id} not found.");
            }

            existingProduct.Name = productDto.Name;
            existingProduct.CategoryId = productDto.CategoryId;

            return await _productRepository.UpdateProductAsync(existingProduct);
        }

        public async Task DeleteProductAsync(int id)
        {
            await _productRepository.DeleteProductAsync(id);
        }
    }
}
