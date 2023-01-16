using WebServices.API.Models.Domain;

namespace WebServices.API.Repositories.CategoryRepository
{
    public interface ICategoryRepository
    {
        public Task<IEnumerable<Category>> GetAllCategories();
        public Task<Category> GetCategoryById(int id);
    }
}
