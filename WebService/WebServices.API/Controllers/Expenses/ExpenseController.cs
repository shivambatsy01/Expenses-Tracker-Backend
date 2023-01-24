using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebServices.API.Models.Domain;
using WebServices.API.Models.RequestDTO;
using WebServices.API.Models.ResponseDTO;
using WebServices.API.Repositories.ExpenseRepository;

namespace WebServices.API.Controllers.Expenses
{
    [ApiController]
    [Route("/expenses")]
    public class ExpenseController : Controller
    {
        private readonly IExpenseRepository expenseRepository;
        private readonly IMapper mapper;
        public ExpenseController(IExpenseRepository expenseRepository, IMapper mapper)
        {
            this.expenseRepository = expenseRepository;
            this.mapper = mapper;
        }



        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllExpenses()
        {
            //or we can use paging here : mention this into jwt string
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var expenses = await expenseRepository.GetAllExpensesAsync();
                return Ok(expenses);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "sorry server down");
            }
        }


        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetExpenseById")]
        public async Task<IActionResult> GetExpenseById(Guid id) //actually not need from requirement perspective
        {
            try
            {
                var expense = await expenseRepository.GetExpenseByIdAsync(id);
                if (expense == null)
                {
                    return NotFound();
                }
                return Ok(expense);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "sorry server down");
            }
        }




        //---------------------------------users-expenses-----------------------------

        [HttpGet]
        [Authorize]
        [Route("users-expenses")]
        public async Task<IActionResult> GetAllUserExpenses()
        {
            //or we can use paging here : mention this into jwt string
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                if(identity == null)
                {
                    return Unauthorized();
                }
                var claims = identity.Claims;
                var id_Claim = identity.FindFirst("Id").Value;
                Guid userId = new Guid(id_Claim);
                var expenses = await expenseRepository.GetUserAllExpensesAsync(userId);
                return Ok(expenses);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "sorry server down");
            }
        }


        [HttpGet]
        [Route("users-expenses/{userId:guid}/{date:DateTime}")]
        public async Task<IActionResult> GetUserExpenseByDate(Guid userId, DateTime date)
        {
            try
            {
                var expenses = await expenseRepository.GetUserExpensesByDateAsync(userId, date);
                return Ok(expenses);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "sorry server down");
            }
        }


        [HttpGet]
        [Route("users-expenses/{userId:guid}/{startDate:DateTime}/{endDate:DateTime}")]
        public async Task<IActionResult> GetUserExpenseBeetweenDates(Guid userId, DateTime startDate, DateTime endDate)
        {
            try
            {
                var expenses = await expenseRepository.GetUserExpensesBeetweenDatesAsync(userId, startDate, endDate);
                return Ok(expenses);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "sorry server down");
            }
        }




        [HttpGet]
        [Route("users-expenses/{userId:guid}/{categoryId:int}")]
        public async Task<IActionResult> GetUserExpenseByCategory(Guid userId, int categoryId)
        {
            try
            {
                var expenses = await expenseRepository.GetUserExpensesByCategoryAsync(userId, categoryId);
                return Ok(expenses);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "sorry server down");
            }
        }



        //-------------------------add/update expense---------------------

        [HttpPost]
        [Route("add-expense/{userId:Guid}")]
        public async Task<IActionResult> AddExpense(Guid userId, ExpenseRequest request)
        {
            try
            {
                if (!AddExpenseRequestValidation(request))
                {
                    return BadRequest(ModelState);
                }
                var expense = mapper.Map<Expense>(request);
                var addedExpense = await expenseRepository.AddExpenseAsync(userId, expense);
                if (addedExpense == null)
                {
                    return StatusCode(StatusCodes.Status503ServiceUnavailable, "Please try again.");
                }

                var response = mapper.Map<ExpenseResponse>(addedExpense);
                return CreatedAtAction(nameof(GetExpenseById), new { id = response.Id }, response);
            }
            catch
            {
                return StatusCode(500, "Server down");
            }
        }



        [HttpPut]
        [Route("{expenseId:guid}")]
        public async Task<IActionResult> UpdateExpense(Guid expenseId, ExpenseRequest request)
        {
            try
            {
                if(!UpdateExpenseRequestValidation(request))
                {
                    return BadRequest(ModelState);
                }

                var expense = mapper.Map<Expense>(request);
                var updatedExpense = await expenseRepository.UpdateExpenseAsync(expenseId, expense);
                if (updatedExpense == null)
                {
                    return NotFound();
                }

                var response = mapper.Map<ExpenseResponse>(updatedExpense);
                return CreatedAtAction(nameof(GetExpenseById), new { id = response.Id }, response);
            }
            catch
            {
                return StatusCode(500, "Server down");
            }
        }


        [HttpDelete]
        [Route("{expenseId:guid}")]
        public async Task<IActionResult> DeleteExpense(Guid expenseId) //only user can delete it's expenses, no ther user -> authorisation
        {
            try
            {
                var deletedExpense = await expenseRepository.DeleteExpenseAsync(expenseId);
                if (deletedExpense == null)
                {
                    return NotFound();
                }

                return Ok(deletedExpense);
            }
            catch
            {
                return StatusCode(500, "Server down");
            }
        }









   //--------------------------validations--------------------------------

        private bool AddExpenseRequestValidation(ExpenseRequest request)
        {
            if (request == null)
            {
                ModelState.AddModelError(nameof(request), "Add request can not be empty");
            }

            if (request.Name == null)
            {
                ModelState.AddModelError(nameof(request.Name), "Expense Name can not be empty");
            }

            if (request.DateOfExpense == null)
            {
                ModelState.AddModelError(nameof(request.DateOfExpense), "Expense Date can not be empty");
            }

            if (request.Amount == null)
            {
                ModelState.AddModelError(nameof(request.Amount), "Expense Amount can not be empty");
            }

            if (request.CategoryId == null)
            {
                ModelState.AddModelError(nameof(request.CategoryId), "Expense Category can not be empty");
            }

            if (request.UserId == null)
            {
                ModelState.AddModelError(nameof(request.UserId), "UserId also required in request body");
            }



            if (ModelState.ErrorCount > 0) return false;
            return true;
        }

        private bool UpdateExpenseRequestValidation(ExpenseRequest request)
        {
            return AddExpenseRequestValidation(request);
        }

    }
}
