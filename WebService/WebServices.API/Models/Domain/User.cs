namespace WebServices.API.Models.Domain
{
    public class User

    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public DateTime DOB { get; set; }
        public DateTime LastLogin { get; set; }

    }
}
