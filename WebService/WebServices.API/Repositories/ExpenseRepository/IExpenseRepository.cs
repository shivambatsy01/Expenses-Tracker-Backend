using WebServices.API.Models.Domain;

namespace WebServices.API.Repositories.ExpenseRepository
{
    public interface IExpenseRepository
    {
        public Task<Expense> GetUserExpenseById(Guid expenseId);
        public Task<IEnumerable<Expense>> GetUserExpenseByDate(DateOnly date);
        public Task<IEnumerable<Expense>> GetUserExpenseBeetweenDates(DateOnly startDate, DateOnly endDate);
        public Task<IEnumerable<Expense>> GetUserExpenseByCategory(int categoryId);
        
        //later we can apply combinations of other filters

    }
}
