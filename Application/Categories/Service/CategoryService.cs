

using Application.Category.Service;
using Domain.Entities.Interfaces;

namespace Application.Categories.Service
{

    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<Domain.Entities.Category>> GetAllCategoriesAsync(int pageNumber, int pageSize)
        {
            return await _categoryRepository.GetAllCategoriesAsync(pageNumber, pageSize);
        }

        public async Task<Domain.Entities.Category> GetCategoryByIdAsync(int id)
        {
            return await _categoryRepository.GetCategoryByIdAsync(id);
        }

        public async Task<Domain.Entities.Category> CreateCategoryAsync(Domain.Entities.Category category)
        {
            return await _categoryRepository.CreateCategoryAsync(category);
        }

        public async Task<Domain.Entities.Category> UpdateCategoryAsync(Domain.Entities.Category category)
        {
            return await _categoryRepository.UpdateCategoryAsync(category);
        }

        public async Task<Domain.Entities.Category> UpsertCategoryAsync(Domain.Entities.Category category)
        {
            return await _categoryRepository.UpsertCategoryAsync(category);
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            return await _categoryRepository.DeleteCategoryAsync(id);
        }
    }
}
