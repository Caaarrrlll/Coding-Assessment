using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechSolutionsCRM.DBContexts;
using TechSolutionsCRM.Models;

namespace TechSolutionsCRM.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomersController : Controller
{
    private readonly TechSolutionsCRMContext _context;

    public CustomersController(TechSolutionsCRMContext context)
    {
        _context = context;
    }

    // GET: Customers
    [HttpGet]
    public async Task<List<Customer>> Index()
    {
        return await _context.Customer.ToListAsync();
    }

    // GET: CustomerDetails?id=5
    [HttpGet("CustomerDetails")]
    public async Task<ActionResult<Customer>> CustomerDetails([FromQuery] int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var customer = await _context.Customer
            .FirstOrDefaultAsync(m => m.Id == id);
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
