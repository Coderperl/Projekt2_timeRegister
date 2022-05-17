using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projekt2_tidrapportering.Data;
using Projekt2_tidrapportering.DTOS.CustomerDTOS;
using Projekt2_tidrapportering.DTOS.ProjectDTOS;
using Projekt2_tidrapportering.DTOS.TimeRegisterDTOS;

namespace Projekt2_tidrapportering.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeRegisterController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TimeRegisterController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetTimeRegisters()
        {
            return Ok(_context.TimeRegisters.Select(t => new TimeRegistersDTO()
            {
                Id = t.Id,
                Date = t.Date,
                Description = t.Description,
                AmountMinutes = t.AmountMinutes,
                Project = new ProjectDTO()
                {
                    Id = t.Project.Id,
                    Name = t.Project.ProjectName,
                    Customer = new CustomerDTO()
                    {
                        Id = t.Customer.Id,
                        Name = t.Customer.Name
                    }
                }
                
            }).ToList());
        }

        [HttpGet]
        [Route("{Id}")]
        public IActionResult GetTimeRegister(int Id)
        {
           
            var timeregister = _context.TimeRegisters
                .Include(c => c.Customer)
                .Include(p => p.Project)
                .FirstOrDefault(t => t.Id == Id);
            if (timeregister == null) return NotFound();
            var registerDTO = new TimeRegisterDTO()
            {
                Id = timeregister.Id,
                Date = timeregister.Date,
                Description = timeregister.Description,
                AmountMinutes = timeregister.AmountMinutes,
                Project = new ProjectDTO()
                {
                    Id = timeregister.Project.Id,
                    Name = timeregister.Project.ProjectName,
                    Customer = new CustomerDTO()
                    {
                        Id = timeregister.Customer.Id,
                        Name = timeregister.Customer.Name
                    }
                }
            };
            return Ok(registerDTO);
        }

        [HttpPost]
        public IActionResult CreateTimeRegister(CreateTimeRegisterDTO createdDTO)
        {
            var cust = _context.Customers.Find(createdDTO.CustomerId);
            var proj = _context.Projects.Find(createdDTO.ProjectId);
            var timeregister = new TimeRegister()
            {
                Date = createdDTO.Date,
                Description = createdDTO.Description,
                AmountMinutes = createdDTO.AmountMinutes,
                Customer = cust,
                Project = proj,
            };
            _context.TimeRegisters.Add(timeregister);
            _context.SaveChanges();
            var TimeRegisterDTO = new TimeRegisterDTO()
            {
                Id = timeregister.Id,
                AmountMinutes = timeregister.AmountMinutes,
                Date = timeregister.Date,
                Description = timeregister.Description,
                Project = new ProjectDTO()
                {
                    Id = proj.Id,
                    Name = proj.ProjectName,
                    Customer = new CustomerDTO()
                    {
                        Id = cust.Id,
                        Name = cust.Name
                    }
                },
            };
            return CreatedAtAction(nameof(GetTimeRegister), new {Id = timeregister.Id}, TimeRegisterDTO);
        }

        [HttpPut]
        [Route("{Id}")]
        public IActionResult UpdateTimeRegister(UpdateTimeRegisterDTO timeRegisterDto, int Id)
        {
            var cust = _context.Customers.Find(timeRegisterDto.CustomerId);
            if (cust == null) return NotFound();
            var proj = _context.Projects.Find(timeRegisterDto.ProjectId);
            if (proj == null) return NotFound();
            var timeregister = _context.TimeRegisters.FirstOrDefault(t => t.Id == Id);
            timeregister.AmountMinutes = timeRegisterDto.AmountMinutes;
            timeregister.Date = timeRegisterDto.Date;
            timeregister.Description = timeRegisterDto.Description;
            timeregister.Customer = cust;
            cust.Id = timeRegisterDto.CustomerId;
            timeregister.Project = proj;
            proj.Id = timeRegisterDto.ProjectId;
            _context.SaveChanges();
            return NoContent();

        }

        [HttpDelete]
        [Route("{Id}")]
        public IActionResult DeleteTimeRegister(int Id)
        {
            var deleteTimeregister = _context.TimeRegisters.FirstOrDefault(t => t.Id == Id);
            if (deleteTimeregister == null) return NotFound();
            _context.TimeRegisters.Remove(deleteTimeregister);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
