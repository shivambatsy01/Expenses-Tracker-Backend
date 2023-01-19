using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebServices.API.Database;
using WebServices.API.Models.Domain;
using WebServices.API.Models.RequestDTO;
using WebServices.API.Repositories.CategoryRepository;

namespace WebServices.API.Controllers
{
    [ApiController]
    [Route("{categories}")]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;
        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                var categories = await categoryRepository.GetAllCategoriesAsync();
                return Ok(categories);
            }
            catch
            {
                return StatusCode(500, "internal server error, please try again.");
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetCategoriesById(int id)
        {
            try
            {
                var category = await categoryRepository.GetCategoryByIdAsync(id);
                if(category == null)
                {
                    return NotFound();
                }
                return Ok(category);
            }
            catch
            {
                return StatusCode(500, "internal server error, please try again.");
            }
        }


        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoryRequest request)
        {
            try
            {
                var category = mapper.Map<Category>(request);
                var addedCategory = await categoryRepository.AddCategoryAsync(category);
                if(addedCategory == null)
                {
                    return StatusCode(StatusCodes.Status503ServiceUnavailable, "please try again later");
                }

                return Ok(addedCategory);
            }
            catch
            {
                return StatusCode(500, "internal server error, please try again.");
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> RemoveCategory(int id)
        {
            try
            {
                var removedCategory = await categoryRepository.RemoveCategoryAsync(id);
                if (removedCategory == null)
                {
                    return NotFound();
                }

                return Ok(removedCategory);
            }
            catch
            {
                return StatusCode(500, "internal server error, please try again.");
            }
        }
    }
}
