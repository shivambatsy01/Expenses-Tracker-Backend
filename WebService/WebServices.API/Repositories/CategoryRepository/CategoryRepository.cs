using Microsoft.EntityFrameworkCore;
using WebServices.API.Database;
using WebServices.API.Models.Domain;

namespace WebServices.API.Repositories.CategoryRepository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly MyDBContext dbContext;
        public CategoryRepository(MyDBContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            return await dbContext.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryById(int id)
        {
            return await dbContext.Categories.FirstOrDefaultAsync(x => x.CategoryId == id);
        }
    }
}
