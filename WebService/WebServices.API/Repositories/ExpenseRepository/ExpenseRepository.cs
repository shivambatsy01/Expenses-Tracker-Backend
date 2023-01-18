using Microsoft.AspNetCore.Mvc;
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

        public async Task<Expense> AddExpenseAsync(Guid userId, Expense expense)
        {
            expense.Id= Guid.NewGuid();
            expense.UserId= userId;
            await dbContext.Expenses.AddAsync(expense);
            await dbContext.SaveChangesAsync();
            return expense;
        }

        public async Task<IEnumerable<Expense>> GetAllExpensesAsync()
        {
            return await dbContext.Expenses.ToListAsync();
        }


        public async Task<IEnumerable<Expense>> GetUserAllExpensesAsync(Guid userId)
        {
            return await dbContext.Expenses.Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task<IEnumerable<Expense>> GetUserExpensesBeetweenDatesAsync(Guid userId, DateTime startDate, DateTime endDate)
        {
            return await dbContext.Expenses.Where(x =>
             x.DateOfExpense.CompareTo(startDate) >= 0 && x.DateOfExpense.CompareTo(endDate) <= 0 && x.UserId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Expense>> GetUserExpensesByCategoryAsync(Guid userId, int categoryId)
        {
            return await dbContext.Expenses.Where(x => x.CategoryId == categoryId && x.UserId == userId).ToListAsync();
        }

        public async Task<IEnumerable<Expense>> GetUserExpensesByDateAsync(Guid userId, DateTime date)
        {
            //need to change this as we are storing time also with date, need to apply some comparisons date+timespan(1 day) something like
            return await dbContext.Expenses.Where(x => x.DateOfExpense.CompareTo(date) == 0 && x.UserId == userId)
                .ToListAsync();
        }

        public async Task<Expense> GetExpenseByIdAsync(Guid expenseId) //need to provide userid also in nosql db
        {
            return await dbContext.Expenses.FirstOrDefaultAsync(x => x.Id == expenseId);
        }

        public async Task<Expense> UpdateExpenseAsync(Guid expenseId, Expense expense)
        {
            var existingExpense = await  dbContext.Expenses.FirstOrDefaultAsync(x => x.Id == expenseId);
            if(existingExpense == null)
            {
                return null;
            }

            existingExpense.DateOfExpense = expense.DateOfExpense;
            existingExpense.Name = expense.Name;
            existingExpense.Amount = expense.Amount;
            existingExpense.Remarks= expense.Remarks;
            existingExpense.CategoryId = expense.CategoryId;

            await dbContext.SaveChangesAsync();
            return existingExpense;
        }


        public async Task<Expense> DeleteExpenseAsync(Guid expenseId)
        {
            var existingExpense = await dbContext.Expenses.FirstOrDefaultAsync(x => x.Id == expenseId);
            if(existingExpense == null)
            {
                return null;
            }

            dbContext.Expenses.Remove(existingExpense);
            await dbContext.SaveChangesAsync();
            return existingExpense;
        }
    }
}
