

using Application.Product.Dto;
using Domain.Entities;

namespace Application.Product.Service
{
    public interface IProductService
    {
        Task<IEnumerable<Domain.Entities.Product>> GetProductsAsync();
        Task<Domain.Entities.Product> GetProductByIdAsync(int id);
        Task<Domain.Entities.Product> CreateProductAsync(Domain.Entities.Product product);
        Task<Domain.Entities.Product> UpdateProductAsync(int id, Domain.Entities.Product product);
        Task DeleteProductAsync(int id);
    }
}
