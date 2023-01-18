using WebServices.API.Models.Domain;

namespace WebServices.API.Repositories.ExpenseRepository
{
    public interface IExpenseRepository
    {
        public Task<IEnumerable<Expense>> GetAllExpensesAsync(); //only for api testing purpose
        public Task<Expense> GetExpenseByIdAsync(Guid expenseId);
        public Task<IEnumerable<Expense>> GetUserAllExpensesAsync(Guid userId);
        public Task<IEnumerable<Expense>> GetUserExpensesByDateAsync(Guid userId, DateTime date);
        public Task<IEnumerable<Expense>> GetUserExpensesBeetweenDatesAsync(Guid usierId, DateTime startDate, DateTime endDate);
        public Task<IEnumerable<Expense>> GetUserExpensesByCategoryAsync(Guid userId, int categoryId);
        public Task<Expense> AddExpenseAsync(Guid userId, Expense expense);
        public Task<Expense> UpdateExpenseAsync(Guid expenseId, Expense expense);
        public Task<Expense> DeleteExpenseAsync(Guid expenseId);
    }
}
