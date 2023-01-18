using AutoMapper;
using WebServices.API.Models.Domain;
using WebServices.API.Models.RequestDTO;

namespace WebServices.API.ProfilesMapping
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoryRequest, Category>();
        }
    }
}
