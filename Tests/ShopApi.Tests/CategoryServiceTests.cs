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
    public class CategoryServiceTests
    {
        [Fact]
        public async Task GetCategoriesAsync_ReturnsCategories()
        {
            // Arrange
            var mockRepository = new Mock<ICategoryRepository>();
            mockRepository.Setup(repo => repo.GetAllCategoriesAsync(1, 10))
                          .ReturnsAsync(GetTestCategories());

            var categoryService = new CategoryService(mockRepository.Object);

            // Act
            var categories = await categoryService.GetAllCategoriesAsync(1, 10);

            // Assert
            Assert.NotNull(categories);
            Assert.Equal(2, categories.Count()); // Adjust based on your test data
        }

        [Fact]
        public async Task CreateCategoryAsync_ReturnsCreatedCategory()
        {
            // Arrange
            var mockRepository = new Mock<ICategoryRepository>();
            mockRepository.Setup(repo => repo.CreateCategoryAsync(It.IsAny<Category>()))
                          .ReturnsAsync((Category c) =>
                          {
                              c.Id = 1; // Simulate creation with Id assigned
                              return c;
                          });

            var categoryService = new CategoryService(mockRepository.Object);
            var newCategory = new Category { Name = "Test Category" };

            // Act
            var createdCategory = await categoryService.CreateCategoryAsync(newCategory);

            // Assert
            Assert.NotNull(createdCategory);
            Assert.Equal(newCategory.Name, createdCategory.Name);
        }


        private IEnumerable<Category> GetTestCategories()
        {
            return new List<Category>
            {
                new Category { Id = 1, Name = "Category 1" },
                new Category { Id = 2, Name = "Category 2" }
            };
        }
    }
}