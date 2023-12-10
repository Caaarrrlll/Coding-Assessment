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
}
