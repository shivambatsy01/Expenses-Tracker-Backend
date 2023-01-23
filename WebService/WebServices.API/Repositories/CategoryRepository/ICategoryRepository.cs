using WebServices.API.Models.Domain;

namespace WebServices.API.Repositories.CategoryRepository
{
    public interface ICategoryRepository
    {
        public Task<IEnumerable<Category>> GetAllCategoriesAsync();
        public Task<Category> GetCategoryByIdAsync(int id);
        public Task<Category> AddCategoryAsync(Category category);
        public Task<Category> RemoveCategoryAsync(int id);
    }
}
