using AutoMapper;
using WebServices.API.Models.Domain;
using WebServices.API.Models.RequestDTO;
using WebServices.API.Models.ResponseDTO;

namespace WebServices.API.ProfilesMapping
{
    public class ExpenseProfile : Profile
    {
        public ExpenseProfile()
        {
            CreateMap<Expense, ExpenseResponse>();

            CreateMap<ExpenseRequest, Expense>();
        }
    }
}
