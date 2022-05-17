using Microsoft.AspNetCore.Mvc;
using Projekt2_tidrapportering.Data;
using Projekt2_tidrapportering.DTOS.CustomerDTOS;
using Projekt2_tidrapportering.DTOS.ProjectDTOS;

namespace Projekt2_tidrapportering.Controllers
{[Route("api/[controller]")]
    [ApiController]
    public class ProjectController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProjectController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetProjects()
        {
            
            return Ok(_context.Projects.Select(p => new ProjectsDTO()
            {
                Id = p.Id,
                Name = p.ProjectName,
                Customer = new CustomerDTO()
                {
                    Id = p.Customer.Id,
                    Name = p.Customer.Name 
                }

            }).ToList());
        }

        [HttpGet]       
        [Route("{Id}")]
        public IActionResult GetProject(int Id)
        {
            var project = _context.Projects.FirstOrDefault(p => p.Id == Id);
            if (project == null) return NotFound();
            var proj = new ProjectDTO()
            {
                Id = project.Id,
                Name = project.ProjectName,
                Customer = new CustomerDTO()
                {
                    Id = project.Customer.Id,
                    Name = project.Customer.Name
                }
            };
            return Ok(proj);
        }

        [HttpPost]
        public IActionResult CreateProject(CreateProjectDTO project)
        {
            var cust = _context.Customers.Find(project.CustomerId);
            if (cust == null) return NotFound();
            var proj = new Project()
            {
                ProjectName = project.Name,
                Customer = cust
            };
            _context.Projects.Add(proj);
            _context.SaveChanges();
            var projectDto = new ProjectDTO()
            {   Id = proj.Id,
                Name = proj.ProjectName,
                Customer = new CustomerDTO()
                {
                    Id = cust.Id,
                    Name = cust.Name
                }
            };
            return CreatedAtAction(nameof(GetProject), new {Id = proj.Id}, projectDto);
        }

        [HttpPut]
        [Route("{Id}")]
        public IActionResult UpdateProject(UpdateProjectDTO projectDto, int Id)
        {
            var cust = _context.Customers.Find(projectDto.CustomerId);
            if (cust == null) return NotFound();
            var project = _context.Projects.FirstOrDefault(p => p.Id == Id);
            if(project == null) return NotFound();

            project.ProjectName = projectDto.Name;
            project.Customer = cust;
            cust.Id = projectDto.CustomerId;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete]
        [Route("{Id}")]
        public IActionResult DeleteProject(int Id)
        {
            var deleteProject = _context.Projects.FirstOrDefault(p => p.Id == Id);
            if(deleteProject == null) return NotFound();
            _context.Projects.Remove(deleteProject);
            _context.SaveChanges();
            return NoContent();
        }

        
    }
}
