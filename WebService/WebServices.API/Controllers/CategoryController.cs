using Microsoft.AspNetCore.Mvc;

namespace WebServices.API.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
