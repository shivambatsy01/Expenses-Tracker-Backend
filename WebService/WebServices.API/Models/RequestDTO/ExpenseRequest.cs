using WebServices.API.Models.Domain;

namespace WebServices.API.Models.RequestDTO
{
    public class ExpenseRequest
    {
        public string Name { get; set; }
        public double Amount { get; set; }
        public DateTime DateOfExpense { get; set; }
        public string Remarks { get; set; }
        public Guid UserId { get; set; }
        public int CategoryId { get; set; }
    }
}
