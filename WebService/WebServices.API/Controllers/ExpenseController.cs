using Microsoft.AspNetCore.Mvc;
using WebServices.API.Repositories.ExpenseRepository;

namespace WebServices.API.Controllers
{
    [ApiController]
    [Route("/expenses")]
    public class ExpenseController : Controller
    {
        private readonly IExpenseRepository expenseRepository;
        public ExpenseController(IExpenseRepository expenseRepository)
        {
            this.expenseRepository = expenseRepository;
        }



        [HttpGet]
        public async Task<IActionResult> GetAllExpenses()
        {
            //or we can use paging here : mention this into jwt string
            try
            {
                var expenses = await expenseRepository.GetAllExpenses();
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
                var expenses = await expenseRepository.GetAllUserExpenses(userId);
                return Ok(expenses);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "sorry server down");
            }
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetExpenseById(Guid id)
        {
            try
            {
                var expense = await expenseRepository.GetUserExpenseById(id);
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
                var expense = await expenseRepository.GetUserExpenseBeetweenDates(userId, startDate, endDate);
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
                var expense = await expenseRepository.GetUserExpenseByDate(userId, date);
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
                var expense = await expenseRepository.GetUserExpenseByCategory(userId, categoryId);
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


    }
}
