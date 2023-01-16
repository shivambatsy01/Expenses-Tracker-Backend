using Microsoft.EntityFrameworkCore;
using WebServices.API.Database;
using WebServices.API.Models.Domain;

namespace WebServices.API.Repositories.ExpenseRepository
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly MyDBContext dbContext;
        public ExpenseRepository(MyDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<Expense>> GetAllExpenses()
        {
            return await dbContext.Expenses.ToListAsync();
        }

        public async Task<IEnumerable<Expense>> GetUserExpenseBeetweenDates(DateOnly startDate, DateOnly endDate)
        {
            return await dbContext.Expenses.Where(x => 
             x.DateOfExpense.CompareTo(startDate) <= 0 && x.DateOfExpense.CompareTo(endDate) >= 0).ToListAsync();
        }

        public async Task<IEnumerable<Expense>> GetUserExpenseByCategory(int categoryId)
        {
            return await dbContext.Expenses.Where(x => x.CategoryId == categoryId).ToListAsync();
        }

        public async Task<IEnumerable<Expense>> GetUserExpenseByDate(DateOnly date)
        {
            return await dbContext.Expenses.Where(x => x.DateOfExpense.CompareTo(date) == 0).ToListAsync();
        }

        public async Task<Expense> GetUserExpenseById(Guid expenseId)
        {
            return await dbContext.Expenses.FirstOrDefaultAsync(x => x.Id == expenseId);
        }
    }
}
