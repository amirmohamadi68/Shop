
using Domain.Entities.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistance.Context;

namespace Persistance.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ShopDbContext _context;

        public ProductRepository(ShopDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Domain.Entities.Product>> GetProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Domain.Entities.Product> GetProductByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<Domain.Entities.Product> CreateProductAsync(Domain.Entities.Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Domain.Entities.Product> UpdateProductAsync(Domain.Entities.Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }
    }
}
