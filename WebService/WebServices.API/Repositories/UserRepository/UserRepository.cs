using Microsoft.EntityFrameworkCore;
using WebServices.API.Database;
using WebServices.API.Models.Domain;

namespace WebServices.API.Repositories.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly MyDBContext dBContext;
        public UserRepository(MyDBContext dbContext)
        {
              this.dBContext = dbContext;
        }


        public async Task<User> AuthenticateUser(string username, string password)
        {
            var user = await dBContext.Users.FirstOrDefaultAsync(x => x.UserName.ToLower() == username.ToLower() && x.Password == password);
            return user;
        }
    }
}
