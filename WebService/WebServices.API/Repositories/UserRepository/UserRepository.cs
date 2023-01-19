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

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await dBContext.Users.ToListAsync();
        }

        public async Task<User> RegisterUser(User user)
        {
            var existingUser = await dBContext.Users.FirstOrDefaultAsync(x => x.Email.ToLower() == user.Email.ToLower());
            if(existingUser != null)
            {
                return null;
            }

            user.Id = new Guid();
            user.UserName = user.Email; //email equals to username
            await dBContext.Users.AddAsync(user);
            await dBContext.SaveChangesAsync();
            return user;
        }
    }
}
