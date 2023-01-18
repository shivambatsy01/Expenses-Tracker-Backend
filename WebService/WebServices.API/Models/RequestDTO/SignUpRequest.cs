namespace WebServices.API.Models.RequestDTO
{
    public class SignUpRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public DateTime DOB { get; set; }

        //public string UserName { get; set; } //will be same as email
    }
}
