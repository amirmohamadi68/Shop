using ShopApi.TemporaryForTDD.Models;

namespace ShopApi.TemporaryForTDD.Services
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync(int pageNumber, int pageSize);
        Task<Category> GetCategoryByIdAsync(int id);
        Task<Category> CreateCategoryAsync(Category category);
        Task<Category> UpdateCategoryAsync(Category category);
        Task<Category> UpsertCategoryAsync(Category category);
        Task<bool> DeleteCategoryAsync(int id);
    }
}
