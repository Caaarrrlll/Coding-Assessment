using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechSolutionsCRM.Interfaces;
using TechSolutionsCRM.Models;

namespace TechSolutionsCRM.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class CustomersController : Controller
{
    readonly ICustomerService _customerService;
    readonly IAddressService _addressService;

    public CustomersController(ICustomerService customerService, IAddressService addressService)
    {
        _customerService = customerService;
        _addressService = addressService;
    }

    #region Customer Endpoints
    [HttpGet("Customers")]
    [ProducesResponseType(typeof(List<Customer>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
    public async Task<List<Customer>> Index()
    {
        return await _customerService.GetAllCustomers();
    }

    [HttpGet("CustomerDetails")]
    [ProducesResponseType(typeof(Customer), StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(NotFoundObjectResult))]
    public async Task<ActionResult<Customer>> CustomerDetails([FromQuery] int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var customer = await _customerService.GetCustomerById(id);

        if (customer == null)
        {
            return NotFound();
        }

        return Ok(customer);
    }

    [HttpPost("CreateCustomer")]
    [ProducesResponseType(typeof(Customer), StatusCodes.Status201Created)]
    [ProducesErrorResponseType(typeof(ConflictObjectResult))]
    public async Task<ActionResult<Customer>> CreateCustomer([FromBody] Customer customer)
    {
        Customer? NewCustomer = null;
        try
        {
            if (ModelState.IsValid)
            {
                NewCustomer = await _customerService.CreateCustomer(customer);
            }
        }
        catch (Exception)
        {
            throw;
        }

        return Ok(NewCustomer);
    }

  
    [HttpPatch("EditCustomer")]
    [ProducesResponseType(typeof(Customer), StatusCodes.Status200OK)]
    public async Task<ActionResult<Customer>> EditCustomer([FromBody] Customer customer)
    { 
        if (ModelState.IsValid)
        {
            var editedCustomer = await _customerService.EditCustomer(customer);
            if(editedCustomer == null)
            {
                return NotFound();
            }

            return Ok(editedCustomer);
        }

        return BadRequest();
    }

    [HttpDelete("DeleteCustomer")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    public async Task<bool> DeleteCustomer(int id)
    {
        return await _customerService.DeleteCustomer(id);
    }

    #endregion

    #region Address Endpoints

    [HttpPost("CreateAddress")]
    [ProducesResponseType(typeof(Address), StatusCodes.Status201Created)]
    [ProducesErrorResponseType(typeof(ConflictObjectResult))]
    public async Task<ActionResult<Address>> CreateAddress([FromBody] Address address)
    {
        Address? newAddress = null;
        try
        {
            if (ModelState.IsValid)
            {
                newAddress = await _addressService.CreateAddress(address);
            }
        }
        catch (Exception)
        {
            throw;
        }

        return Ok(newAddress);
    }

    [HttpPatch("EditAddress")]
    [ProducesResponseType(typeof(Address), StatusCodes.Status200OK)]
    public async Task<ActionResult<Address>> EditAddress([FromBody] Address address)
    {
        if (ModelState.IsValid)
        {
            var editedAddress = await _addressService.EditAddress(address);
            if (editedAddress == null)
            {
                return NotFound();
            }

            return Ok(editedAddress);
        }

        return BadRequest();
    }

    [HttpDelete("DeleteAddress")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    public async Task<bool> DeleteAddress(int id)
    {
        return await _addressService.DeleteAddress(id);
    }

    #endregion
}
