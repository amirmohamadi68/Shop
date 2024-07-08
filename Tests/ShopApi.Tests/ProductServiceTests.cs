using Moq;
using ShopApi.TemporaryForTDD.Models;
using ShopApi.TemporaryForTDD.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApi.Tests
{
    public class ProductServiceTests
    {
        [Fact]
        public async Task GetProductsAsync_ReturnsProducts()
        {
            // Arrange
            var mockRepository = new Mock<IProductRepository>();
            mockRepository.Setup(repo => repo.GetProductsAsync())
                          .ReturnsAsync(GetTestProducts());

            var productService = new ProductService(mockRepository.Object);

            // Act
            var products = await productService.GetProductsAsync();

            // Assert
            Assert.NotNull(products);
            Assert.Equal(2, products.Count()); // Adjust based on your test data
        }

        [Fact]
        public async Task CreateProductAsync_ReturnsCreatedProduct()
        {
            // Arrange
            var mockRepository = new Mock<IProductRepository>();
            mockRepository.Setup(repo => repo.CreateProductAsync(It.IsAny<Product>()))
                          .ReturnsAsync((Product p) =>
                          {
                              p.Id = 1; // Simulate creation with Id assigned
                              return p;
                          });

            var productService = new ProductService(mockRepository.Object);
            var newProduct = new ProductDto { Name = "Test Product" };

            // Act
            var createdProduct = await productService.CreateProductAsync(newProduct);

            // Assert
            Assert.NotNull(createdProduct);
            Assert.Equal(newProduct.Name, createdProduct.Name);
            Assert.NotNull(createdProduct.Id);
        }


        private IEnumerable<Product> GetTestProducts()
        {
            return new List<Product>
            {
                new Product { Id = 1, Name = "Product 1", CategoryId = 1 },
                new Product { Id = 2, Name = "Product 2", CategoryId = 2 }
            };
        }
    }
}

