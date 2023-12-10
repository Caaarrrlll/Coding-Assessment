using TechSolutionsCRM.Models;

namespace TechSolutionsCRM.Interfaces;

public interface ICustomerService
{
    #region Customer Methods
    Task<List<Customer>> GetAllCustomers();
    Task<Customer?> GetCustomerById(int? id, CancellationToken cancellationToken = default);
    Task<Customer?> CreateCustomer(Customer newCustomer);
    Task<Customer?> EditCustomer(Customer customer);
    Task<bool> DeleteCustomer(int? id);
    #endregion

    #region Address Methods
    #endregion
}
