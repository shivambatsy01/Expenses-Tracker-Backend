using WebServices.API.Models.Domain;

namespace WebServices.API.Repositories.ExpenseRepository
{
    public interface IExpenseRepository
    {
        public Task<IEnumerable<Expense>> GetAllExpenses(); //only for api testing purpose
        public Task<Expense> GetUserExpenseById(Guid expenseId);
        public Task<IEnumerable<Expense>> GetAllUserExpenses(Guid userId);
        public Task<IEnumerable<Expense>> GetUserExpenseByDate(Guid userId, DateTime date);
        public Task<IEnumerable<Expense>> GetUserExpenseBeetweenDates(Guid usierId, DateTime startDate, DateTime endDate);
        public Task<IEnumerable<Expense>> GetUserExpenseByCategory(Guid userId, int categoryId);
        
        //later we can apply combinations of other filters

    }
}
