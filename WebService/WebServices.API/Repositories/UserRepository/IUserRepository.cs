using WebServices.API.Models.Domain;

namespace WebServices.API.Repositories.UserRepository
{
    public interface IUserRepository
    {
        public Task<User> AuthenticateUser(string username, string password);
        public Task<IEnumerable<User>> GetAllUsers();
        public Task<User> RegisterUser(User user);
    }
}
