using AutoMapper;
using TechSolutionsCRM.Models;

namespace TechSolutionsCRM.Mappings;

public class AddressMapper : Profile
{
    public AddressMapper()
    {
        CreateMap<Address, Address>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}
