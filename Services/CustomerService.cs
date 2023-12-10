using Microsoft.EntityFrameworkCore;
using TechSolutionsCRM.DBContexts;
using TechSolutionsCRM.Interfaces;
using TechSolutionsCRM.Models;

namespace TechSolutionsCRM.Services;

public class CustomerService : ICustomerService
{
    protected TechSolutionsCRMContext _context;

    public CustomerService(TechSolutionsCRMContext context) { 
        _context = context;
    }

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

    public async Task<Customer> CreateCustomer(Customer newCustomer)
    {
        var checkExisting = await _context.Customer.Where(c => c.IdentityNumber == newCustomer.IdentityNumber).FirstOrDefaultAsync();

        if (checkExisting != null)
        {
            throw new Exception("User already Exists");
        }

        _context.Customer.Add(newCustomer);
        await _context.SaveChangesAsync();

        return await _context.Customer.Where(c => c.IdentityNumber == newCustomer.IdentityNumber).FirstOrDefaultAsync();
    }
}
