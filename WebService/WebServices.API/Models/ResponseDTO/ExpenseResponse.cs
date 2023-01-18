using WebServices.API.Models.Domain;

namespace WebServices.API.Models.ResponseDTO
{
    public class ExpenseResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Amount { get; set; }
        public DateTime DateOfExpense { get; set; }
        public string Remarks { get; set; }
        public Guid UserId { get; set; }
        public int CategoryId { get; set; }


        //navigation properties

        public User User { get; set; }
        public Category Category { get; set; }
    }
}
