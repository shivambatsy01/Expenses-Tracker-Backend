using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebServices.API.Models.Domain;
using WebServices.API.Models.RequestDTO;
using WebServices.API.Models.ResponseDTO;
using WebServices.API.Repositories.ExpenseRepository;

namespace WebServices.API.Controllers
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
        public async Task<IActionResult> GetAllExpenses()
        {
            //or we can use paging here : mention this into jwt string
            try
            {
                var expenses = await expenseRepository.GetAllExpensesAsync();
                return Ok(expenses);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "sorry server down");
            }
        }


        [HttpGet]
        [Route("{users-expenses}/{userId:Guid}")]
        public async Task<IActionResult> GetAllUserExpenses(Guid userId)
        {
            //or we can use paging here : mention this into jwt string
            try
            {
                var expenses = await expenseRepository.GetAllUserExpensesAsync(userId);
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
        public async Task<IActionResult> GetExpenseById(Guid id)
        {
            try
            {
                var expense = await expenseRepository.GetUserExpenseByIdAsync(id);
                if(expense == null)
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



        [HttpGet]
        [Route("{userId:guid}/{startDate:DateTime}/{endDate:DateTime}")]
        public async Task<IActionResult> GetUserExpenseBeetweenDates(Guid userId, DateTime startDate, DateTime endDate)
        {
            try
            {
                var expense = await expenseRepository.GetUserExpenseBeetweenDatesAsync(userId, startDate, endDate);
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


        [HttpGet]
        [Route("{userId:guid}/{date:DateTime}")]
        public async Task<IActionResult> GetUserExpenseByDate(Guid userId, DateTime date)
        {
            try
            {
                var expense = await expenseRepository.GetUserExpenseByDateAsync(userId, date);
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



        [HttpGet]
        [Route("{userId:guid}/{categoryId:int}")]
        public async Task<IActionResult> GetUserExpenseByCategory(Guid userId, int categoryId)
        {
            try
            {
                var expense = await expenseRepository.GetUserExpenseByCategoryAsync(userId, categoryId);
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


        [HttpPost]
        [Route("{add-expenses}/{userId:Guid}")]
        public async Task<IActionResult> AddExpense(Guid userId, ExpenseRequest request)
        {
            try
            {
                var expense = mapper.Map<Expense>(request);
                var addedExpense = await expenseRepository.AddExpenseAsync(userId, expense);
                if(addedExpense == null)
                {
                    return StatusCode(StatusCodes.Status503ServiceUnavailable, "Please try again.");
                }

                var response = mapper.Map<ExpenseResponse>(addedExpense);
                return CreatedAtAction(nameof(GetExpenseById), new { id = response.Id, response });
            }
            catch
            {
                return StatusCode(500, "Server down");
            }
        }



        [HttpPost]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateExpense(Guid expenseId, ExpenseRequest request)
        {
            try
            {
                var expense = mapper.Map<Expense>(request);
                var updatedExpense = await expenseRepository.UpdateExpenseAsync(expenseId, expense);
                if (updatedExpense == null)
                {
                    return NotFound();
                }

                var response = mapper.Map<ExpenseResponse>(updatedExpense);
                return CreatedAtAction(nameof(GetExpenseById), new { id = response.Id, response });
            }
            catch
            {
                return StatusCode(500, "Server down");
            }
        }





        private bool AddRequestValidation(ExpenseRequest request)
        {
            return true;
        }

    }
}
