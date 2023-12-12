using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechSolutionsCRM.DBContexts;
using TechSolutionsCRM.Interfaces;
using TechSolutionsCRM.Models;

namespace TechSolutionsCRM.Services;

public class AddressService: IAddressService
{
    protected TechSolutionsCRMContext _context;
    private readonly IMapper _mapper;

    public AddressService(TechSolutionsCRMContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }


    #region Public Address Methods
    public async Task<Address?> CreateAddress([FromBody] Address NewAddress)
    {
        Address newMapped = _mapper.Map<Address>(NewAddress);

        _context.Addresses.Add(NewAddress);
        await _context.SaveChangesAsync();

        return await _context.Addresses.Where(a => a.CustomerId == NewAddress.CustomerId).FirstOrDefaultAsync();
    }

    public async Task<Address?> EditAddress(Address UpdateAddress)
    {
        var ExistingAddress = await _context.Addresses.Where(c => c.Id == UpdateAddress.Id).FirstOrDefaultAsync();

        if (ExistingAddress == null)
        {
            return ExistingAddress;
        }

        ExistingAddress = _mapper.Map(UpdateAddress, ExistingAddress);

        _context.Addresses.Update(ExistingAddress);
        await _context.SaveChangesAsync();

        return await _context.Addresses.Where(c => c.Id == UpdateAddress.Id).FirstOrDefaultAsync();
    }

    public async Task<bool> DeleteAddress(int? id)
    {
        var existingAddress = await _context.Addresses.Where(c => c.Id == id).FirstOrDefaultAsync();

        if (existingAddress == null)
        {
            return true;
        }

        _context.Addresses.Remove(existingAddress);
        await _context.SaveChangesAsync();
        return true;
    }
    #endregion
}
