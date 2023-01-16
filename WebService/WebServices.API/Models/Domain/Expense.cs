namespace WebServices.API.Models.Domain
{
    public class Expense
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Amount { get; set; }
        public DateOnly DateOfExpense { get; set; }
        public string Remarks { get; set; }
        public Guid UserId { get; set; }
        public int CategoryId { get; set; }


        //navigation properties

        public User User { get; set; }
        public Category Category { get; set; }
        
    }
}
