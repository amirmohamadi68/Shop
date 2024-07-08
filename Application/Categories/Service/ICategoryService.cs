using Domain.Entities;

namespace Application.Category.Service
{
    public interface ICategoryService
    {
        Task<IEnumerable<Domain.Entities.Category>> GetAllCategoriesAsync(int pageNumber, int pageSize);
        Task<Domain.Entities.Category> GetCategoryByIdAsync(int id);
        Task<Domain.Entities.Category> GetCategoryByNameAsync(string name);

        Task<Domain.Entities.Category> CreateCategoryAsync(Domain.Entities.Category category);
        Task<Domain.Entities.Category> UpdateCategoryAsync(Domain.Entities.Category category);
        Task<Domain.Entities.Category> UpsertCategoryAsync(Domain.Entities.Category category);
        Task<bool> DeleteCategoryAsync(int id);
    }
}