using AutoMapper;
using WebServices.API.Models.Domain;
using WebServices.API.Models.RequestDTO;

namespace WebServices.API.ProfilesMapping
{
    public class SignupProfile : Profile
    {
        public SignupProfile()
        {
            CreateMap<SignUpRequest, User>();
        }
    }
}
