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
        public async Task<IActionResult> GetAllExpenses()  //use only for API testing purpose, dont include in real calla
        {
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
    }
}
