using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TechSolutionsCRM.DBContexts;
using TechSolutionsCRM.Interfaces;
using TechSolutionsCRM.Models;

namespace TechSolutionsCRM.Services;

public class CustomerService : ICustomerService
{
    protected TechSolutionsCRMContext _context;
    private readonly IMapper _mapper;

    public CustomerService(TechSolutionsCRMContext context, IMapper mapper) { 
        _context = context;
        _mapper = mapper;
    }

    #region Public Customer Methods
    public async Task<List<Customer>> GetAllCustomers()
    {
        return await _context.Customer.Include(a => a.Addresses).AsNoTracking().ToListAsync();
    }

    public async Task<Customer?> GetCustomerById(int? id, CancellationToken cancellationToken = default)
    {
        if (id == null) throw new Exception("Id cannot be null when requesting details on specific user");

        return await _context.Customer.Where(c => c.Id == id)
            .Include(a => a.Addresses)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<Customer?> CreateCustomer(Customer newCustomer)
    {
        var checkExisting = await _context.Customer.Where(c => c.IdentityNumber == newCustomer.IdentityNumber).FirstOrDefaultAsync();

        if (checkExisting != null)
        {
            return null;
        }

        _context.Customer.Add(newCustomer);
        await _context.SaveChangesAsync();

        return await _context.Customer.Where(c => c.IdentityNumber == newCustomer.IdentityNumber).FirstOrDefaultAsync();
    }

    public async Task<Customer?> EditCustomer(Customer UpdatedCustomer)
    {
        var ExistingCustomer = await _context.Customer.Where(c => c.Id == UpdatedCustomer.Id).FirstOrDefaultAsync();

        if(ExistingCustomer == null)
        {
            return ExistingCustomer;
        }

        ExistingCustomer = _mapper.Map(UpdatedCustomer, ExistingCustomer);

        _context.Customer.Update(ExistingCustomer);
        await _context.SaveChangesAsync();

        return await _context.Customer.Where(c => c.Id == UpdatedCustomer.Id).FirstOrDefaultAsync();
    }

    public async Task<bool> DeleteCustomer(int? id)
    {
        var existingCustomer = await _context.Customer.Where(c => c.Id == id).FirstOrDefaultAsync();
        
        if (existingCustomer == null)
        {
            return true;
        }

        _context.Customer.Remove(existingCustomer);
        await _context.SaveChangesAsync();
        return true;
    }
    #endregion

    #region Public Address Methods
    #endregion
}
