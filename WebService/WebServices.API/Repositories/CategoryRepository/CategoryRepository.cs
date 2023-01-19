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



        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await dbContext.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await dbContext.Categories.FirstOrDefaultAsync(x => x.CategoryId == id);
        }

        public async Task<Category> AddCategoryAsync(Category category) //need to handle properly
        {
            var find = await dbContext.Categories.FirstOrDefaultAsync(x => x.CategoryName.ToLower() == category.CategoryName.ToLower());
            if(find != null)
            {
                find.CategoryName += ": Category Already present, cannot add other with same name ";
                return find;
            }

            await dbContext.Categories.AddAsync(category);
            await dbContext.SaveChangesAsync();
            return category;
        }

        public async Task<Category> RemoveCategoryAsync(int id)
        {
            var category = await dbContext.Categories.FirstOrDefaultAsync(x => x.CategoryId == id);
            if (category == null) return null;

            dbContext.Categories.Remove(category);
            await dbContext.SaveChangesAsync();
            return category;
        }
    }
}
