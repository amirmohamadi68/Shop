using ShopApi.TemporaryForTDD.Models;

namespace ShopApi.TemporaryForTDD.Services
{
  
        public class CategoryService : ICategoryService
        {
            private readonly ICategoryRepository _categoryRepository;

            public CategoryService(ICategoryRepository categoryRepository)
            {
                _categoryRepository = categoryRepository;
            }

            public async Task<IEnumerable<Category>> GetAllCategoriesAsync(int pageNumber, int pageSize)
            {
                return await _categoryRepository.GetAllCategoriesAsync(pageNumber, pageSize);
            }

            public async Task<Category> GetCategoryByIdAsync(int id)
            {
                return await _categoryRepository.GetCategoryByIdAsync(id);
            }

            public async Task<Category> CreateCategoryAsync(Category category)
            {
                return await _categoryRepository.CreateCategoryAsync(category);
            }

            public async Task<Category> UpdateCategoryAsync(Category category)
            {
                return await _categoryRepository.UpdateCategoryAsync(category);
            }

            public async Task<Category> UpsertCategoryAsync(Category category)
            {
                return await _categoryRepository.UpsertCategoryAsync(category);
            }

            public async Task<bool> DeleteCategoryAsync(int id)
            {
                return await _categoryRepository.DeleteCategoryAsync(id);
            }
        }
    }
