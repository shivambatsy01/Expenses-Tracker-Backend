using WebServices.API.Models.Domain;

namespace WebServices.API.Repositories.TokenHandlerRepository
{
    public interface ITokenHandler
    {
        public Task<string> CreateTokenAsync(User user);
    }
}
