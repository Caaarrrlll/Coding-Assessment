using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechSolutionsCRM.DBContexts;
using TechSolutionsCRM.Interfaces;
using TechSolutionsCRM.Models;

namespace TechSolutionsCRM.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomersController : Controller
{
    private readonly TechSolutionsCRMContext _context;
    readonly ICustomerService _customerService;

    public CustomersController(TechSolutionsCRMContext context, ICustomerService customerService)
    {
        _context = context;
        _customerService = customerService;
    }

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

        return customer;
    }

    // POST: Customers/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost("CreateCustomer")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Name,Surname,Email,PhoneNumber")] Customer customer)
    {
        if (ModelState.IsValid)
        {
            _context.Add(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(customer);
    }

  
    // POST: Customers/EditCustomer
    [HttpPatch("EditCustomer")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit([Bind("Id,Name,Surname,Email,PhoneNumber")] Customer customer)
    { 
        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(customer);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(customer.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(customer);
    }

    // POST: Customers/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var customer = await _context.Customer.FindAsync(id);
        if (customer != null)
        {
            _context.Customer.Remove(customer);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool CustomerExists(int id)
    {
        return _context.Customer.Any(e => e.Id == id);
    }
}
