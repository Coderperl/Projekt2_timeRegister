using Customer_Project_Administration_Application.Services;
using Customer_Project_Administration_Application.ViewModels.ProjectFolder;
using Microsoft.AspNetCore.Mvc;
using Projekt2_tidrapportering.DTOS.CustomerDTOS;
using Projekt2_tidrapportering.DTOS.ProjectDTOS;

namespace Customer_Project_Administration_Application.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IProjectService _service;

        public ProjectController(IProjectService service)
        {
            _service = service;
        }
        public IActionResult Index()
        {
            var model = new ProjectsViewModel();
            model.Projects = _service.GetProjects()
                
                .Select(p => new ProjectsViewModel.ProjectItemViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Customer = new CustomerDTO()
                    {
                        Name = p.Customer.Name
                    }
                }).OrderBy(p => p.Customer.Name).ToList();
            return View(model);
        }
    }
}
