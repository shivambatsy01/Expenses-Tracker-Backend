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
        [Route("{users-expenses}/{userId:Guid}")]
        public async Task<IActionResult> GetAllUserExpenses(Guid userId)
        {
            //or we can use paging here : mention this into jwt string
            try
            {
                var expenses = await expenseRepository.GetUserAllExpensesAsync(userId);
                return Ok(expenses);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "sorry server down");
            }
        }


        [HttpGet]
        [Route("{users-expenses}/{userId:guid}/{date:DateTime}")]
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
        [Route("{users-expenses}/{userId:guid}/{startDate:DateTime}/{endDate:DateTime}")]
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
        [Route("{users-expenses}/{userId:guid}/{categoryId:int}")]
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
        [Route("{add-expense}/{userId:Guid}")]
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
                return CreatedAtAction(nameof(GetExpenseById), new { id = response.Id}, response);
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
        public async Task<IActionResult> DeleteExpense(Guid expenseId)
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



        private bool AddRequestValidation(ExpenseRequest request)
        {
            return true;
        }

    }
}
