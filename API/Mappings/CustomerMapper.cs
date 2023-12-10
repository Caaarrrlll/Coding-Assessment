using AutoMapper;
using TechSolutionsCRM.Models;

namespace TechSolutionsCRM.Mappings;

public class CustomerMapper : Profile
{
    public CustomerMapper()
    {
        CreateMap<Customer, Customer>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}
