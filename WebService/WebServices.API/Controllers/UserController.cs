using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebServices.API.Repositories.UserRepository;

namespace WebServices.API.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController : Controller
    {
        private readonly IUserRepository userRepository;
        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await userRepository.GetAllUsers();
                return Ok(users);
            }
            catch
            {
                return StatusCode(500, "internal server error");
            }
        }
    }
}
