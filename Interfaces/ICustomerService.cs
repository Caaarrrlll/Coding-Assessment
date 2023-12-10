using TechSolutionsCRM.Models;

namespace TechSolutionsCRM.Interfaces;

public interface ICustomerService
{
    Task<List<Customer>> GetAllCustomers();
    Task<Customer?> GetCustomerById(int? id, CancellationToken cancellationToken = default);
    Task<Customer> CreateCustomer(Customer newCustomer);
}
