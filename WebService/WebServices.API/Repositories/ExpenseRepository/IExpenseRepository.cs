using WebServices.API.Models.Domain;

namespace WebServices.API.Repositories.ExpenseRepository
{
    public interface IExpenseRepository
    {
        public Task<IEnumerable<Expense>> GetAllExpensesAsync(); //only for api testing purpose
        public Task<Expense> GetUserExpenseByIdAsync(Guid expenseId);
        public Task<IEnumerable<Expense>> GetAllUserExpensesAsync(Guid userId);
        public Task<IEnumerable<Expense>> GetUserExpenseByDateAsync(Guid userId, DateTime date);
        public Task<IEnumerable<Expense>> GetUserExpenseBeetweenDatesAsync(Guid usierId, DateTime startDate, DateTime endDate);
        public Task<IEnumerable<Expense>> GetUserExpenseByCategoryAsync(Guid userId, int categoryId);
        public Task<Expense> AddExpenseAsync(Guid userId, Expense expense);
        public Task<Expense> UpdateExpenseAsync(Guid expenseId, Expense expense);

    }
}
