

using Domain.Entities.Interfaces;

namespace Application.Product.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Domain.Entities.Product>> GetProductsAsync()
        {
            return await _productRepository.GetProductsAsync();
        }

        public async Task<Domain.Entities.Product> GetProductByIdAsync(int id)
        {
            return await _productRepository.GetProductByIdAsync(id);
        }

        public async Task<Domain.Entities.Product> CreateProductAsync(Domain.Entities.Product productDto)
        {
           // var product = Domain.Entities.Product.Create(productDto.Name, productDto.CategoryId);
           
            return await _productRepository.CreateProductAsync(productDto);
        }

        public async Task<Domain.Entities.Product> UpdateProductAsync(int id, Domain.Entities.Product productDto)
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
