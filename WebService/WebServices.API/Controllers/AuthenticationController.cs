using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
//using System.Web.Mvc;
using WebServices.API.Database;
using WebServices.API.Models.Domain;
using WebServices.API.Models.RequestDTO;
using WebServices.API.Repositories.TokenHandlerRepository;
using WebServices.API.Repositories.UserRepository;

namespace WebServices.API.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthenticationController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly ITokenHandler tokenHandler;
        private readonly IMapper mapper;
        public AuthenticationController(IUserRepository userRepository, IMapper mapper, ITokenHandler tokenHandler)
        {
            this.userRepository = userRepository;
            this.tokenHandler = tokenHandler;
            this.mapper = mapper;
        }


        [HttpPost]
        public async Task<IActionResult> RegisterUser(SignUpRequest request)
        {
            try
            {
                if(!SignupValidations(request))
                {
                    return BadRequest(ModelState);
                }

                var user = mapper.Map<User>(request);
                var addedUser = await userRepository.RegisterUser(user);
                if(addedUser == null)
                {
                    return StatusCode(StatusCodes.Status409Conflict, $"User already registered with email: {request.Email}");
                }

                return Ok(addedUser);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error.");
            }
        }


        [HttpPost]
        [Route("{login}")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            try
            {
                if(!ValidateLoginRequest(request))
                {
                    return BadRequest(ModelState);
                }

                var user = await userRepository.AuthenticateUser(request.UserName, request.Password);
                if(user != null)
                {
                    string token = await tokenHandler.CreateTokenAsync(user);
                    return Ok(token);
                }
                
                return BadRequest("Incorrect username or password");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error.");
            }
        }



        private bool SignupValidations(SignUpRequest request)
        {
            if(request == null)
            {
                ModelState.AddModelError(nameof(request), "request body can not be empty.");
            }

            if(request.Name == null)
            {
                ModelState.AddModelError(nameof(request.Name), $"{nameof(request.Name)} must have value.");
            }

            if (request.Email == null)
            {
                ModelState.AddModelError(nameof(request.Email), $"{nameof(request.Email)} must have a valid Email address.");
            }

            if (request.DOB == null)
            {
                ModelState.AddModelError(nameof(request.DOB), $"{nameof(request.DOB)} must have value.");
            }

            if (request.Password == null)
            {
                ModelState.AddModelError(nameof(request.Password), $"{nameof(request.Password)} must have value.");
            }

            if(ModelState.ErrorCount > 0)
            {
                return false;
            }
            return true;

        }


        private bool ValidateLoginRequest(LoginRequest request)
        {
            if(request.UserName == null)
            {
                ModelState.AddModelError(nameof(request.UserName), $"{nameof(request.UserName)} can not be empty");
            }
            if (request.Password == null)
            {
                ModelState.AddModelError(nameof(request.Password), $"{nameof(request.Password)} can not be empty");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }
            return true;

        }
    }
}
