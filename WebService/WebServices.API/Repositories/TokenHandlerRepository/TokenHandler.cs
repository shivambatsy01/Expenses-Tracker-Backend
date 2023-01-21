using WebServices.API.Models.Domain;

namespace WebServices.API.Repositories.TokenHandlerRepository
{
    public class TokenHandler : ITokenHandler
    {
        private readonly IConfiguration configuration;
        public TokenHandler(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public Task<string> CreateTokenAsync(User user)
        {
            throw new NotImplementedException();
        }

        public void fun()
        {
            throw new NotImplementedException(); ;
        }
    }
}
