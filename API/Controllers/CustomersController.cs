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

    public CustomersController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet("Customers")]
    [ProducesResponseType(typeof(List<Customer>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
    public async Task<List<Customer>> Index()
    {
        return await _customerService.GetAllCustomers();
    }

    [HttpGet("{id}",Name = "CustomerDetails")]
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

        return customer;
    }

    [HttpPost("CreateCustomer")]
    [ProducesResponseType(typeof(Customer), StatusCodes.Status201Created)]
    [ProducesErrorResponseType(typeof(ConflictObjectResult))]
    public async Task<ActionResult<Customer>> Create([Bind("Name,Surname,Email,PhoneNumber")] Customer customer)
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

        return CreatedAtRoute(routeName: "CustomerDetails", routeValues: new { id = NewCustomer!.Id }, value: NewCustomer);
    }

  
    [HttpPatch("EditCustomer")]
    [ProducesResponseType(typeof(Customer), StatusCodes.Status200OK)]
    public async Task<ActionResult<Customer>> Edit([Bind("Id,Name,Surname,Email,PhoneNumber")] Customer customer)
    { 
        if (ModelState.IsValid)
        {
            var editedCustomer = await _customerService.EditCustomer(customer);
            if(editedCustomer == null)
            {
                return NotFound();
            }

            return CreatedAtRoute(routeName: "CustomerDetails", routeValues: new { id = customer.Id }, value: editedCustomer);
        }

        return BadRequest();
    }

    [HttpDelete("DeleteCustomer")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    public async Task<bool> DeleteCustomer(int id)
    {
        return await _customerService.DeleteCustomer(id);
    }
}
