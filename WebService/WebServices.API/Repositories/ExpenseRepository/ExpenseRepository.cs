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


        public async Task<IEnumerable<Expense>> GetAllUserExpenses(Guid userId)
        {
            return await dbContext.Expenses.Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task<IEnumerable<Expense>> GetUserExpenseBeetweenDates(Guid userId, DateTime startDate, DateTime endDate)
        {
            return await dbContext.Expenses.Where(x =>
             x.DateOfExpense.CompareTo(startDate) >= 0 && x.DateOfExpense.CompareTo(endDate) <= 0 && x.UserId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Expense>> GetUserExpenseByCategory(Guid userId, int categoryId)
        {
            return await dbContext.Expenses.Where(x => x.CategoryId == categoryId && x.UserId == userId).ToListAsync();
        }

        public async Task<IEnumerable<Expense>> GetUserExpenseByDate(Guid userId, DateTime date)
        {
            //need to change this as we are storing time also with date, need to apply some comparisons date+timespan(1 day) something like
            return await dbContext.Expenses.Where(x => x.DateOfExpense.CompareTo(date) == 0 && x.UserId == userId)
                .ToListAsync();
        }

        public async Task<Expense> GetUserExpenseById(Guid expenseId)
        {
            return await dbContext.Expenses.FirstOrDefaultAsync(x => x.Id == expenseId);
        }
    }
}
