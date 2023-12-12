using TechSolutionsCRM.Models;

namespace TechSolutionsCRM.Interfaces;

public interface IAddressService
{
    Task<Address?> CreateAddress(Address Address);
    Task<Address?> EditAddress(Address Address);
    Task<bool> DeleteAddress(int? id);
}
