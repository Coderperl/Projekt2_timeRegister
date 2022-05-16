using Microsoft.AspNetCore.Mvc;
using Projekt2_tidrapportering.Data;
using Projekt2_tidrapportering.DTOS.CustomerDTOS;

namespace Projekt2_tidrapportering.Controllers
{[Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomerController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetCustomers()
        {
            return Ok(_context.Customers.Select(c => new CustomersDTO()
            {
                Name = c.Name,
                Id = c.Id
            }).ToList());
        }

        [HttpGet]
        [Route("{Id}")]
        public IActionResult GetCustomer(int Id)
        {
            var customer = _context.Customers.FirstOrDefault(c => c.Id == Id);
            if (customer == null)
                return NotFound();
            var cust = new CustomerDTO()
            {
                Id = customer.Id,
                Name = customer.Name,
            };
            return Ok(cust);
        }

        [HttpPost]
        public IActionResult CreateCustomer(CreateCustomerDTO customer)
        {
            var cust = new Customer()
            {
                Name = customer.Name,
            };
            _context.Customers.Add(cust);
            _context.SaveChanges();
            var custDTO = new CustomerDTO()
            {
                Id = cust.Id,
                Name = cust.Name,
            };
            return CreatedAtAction(nameof(GetCustomer), new {Id = cust.Id}, custDTO);
        }

        [HttpPut]
        [Route("{Id}")]
        public IActionResult UpdateCustomer(UpdateCustomerDTO customer, int Id)
        {
            var getCust = _context.Customers.FirstOrDefault(c => c.Id == Id);
            if (getCust == null)
                return NotFound();
            getCust.Name = customer.Name;
            _context.SaveChanges();
            return NoContent();
        } 

        [HttpDelete]
        [Route("{Id}")]
        public IActionResult DeleteCustomer(int Id)
        {
            var cust = _context.Customers.FirstOrDefault(c => c.Id == Id);
            if (cust == null) return NotFound();
            _context.Customers.Remove(cust);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
