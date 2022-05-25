using System.Reflection.Metadata.Ecma335;
using Customer_Project_Administration_Application.Services;
using Customer_Project_Administration_Application.ViewModels.ProjectFolder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Projekt2_tidrapportering.DTOS.CustomerDTOS;
using Projekt2_tidrapportering.DTOS.ProjectDTOS;

namespace Customer_Project_Administration_Application.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProjectController : Controller
    {
        private readonly IProjectService _service;
        private readonly ICustomerService _customerService;

        public ProjectController(IProjectService service, ICustomerService customerService)
        {
            _service = service;
            _customerService = customerService;
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
        [HttpGet]
        public IActionResult Create()
        {
            return View(new CreateProjectViewModel
            {
                Customers = SetList()
            });
        }
        [HttpPost]
        public IActionResult Create(CreateProjectDTO project)
        {
            if (ModelState.IsValid)
            {
                _service.CreateProject(project);
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError(nameof(project.CustomerId), "Please select a customer.");
            
            return View(new CreateProjectViewModel{Customers = SetList()});
        }
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var proj = _service.GetProjects().First(c => c.Id == Id);
            var model = new EditProjectViewModel
            {
                Name = proj.Name,
                CustomerId = proj.Customer.Id,
                Customers = SetList()
            };
            
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(UpdateProjectDTO project, int Id)
        {
            if (ModelState.IsValid)
            {
                _service.GetProjects().First(c => c.Id == Id);
                if (project.CustomerId == 0)
                {
                    ModelState.AddModelError(nameof(project.CustomerId), "Please select customer");
                    return View(new EditProjectViewModel{Customers = SetList()});
                }
                var result =  _service.UpdateProject(project, Id);
               if (result == Status.Error)
               {
                   ModelState.AddModelError(string.Empty, "Something went wrong.");
                   return View(new EditProjectViewModel { Customers = SetList() });
                }
            }
            ModelState.AddModelError(nameof(project.Name), "Something went wrong");
            return View(new EditProjectViewModel { Customers = SetList() });
        }
        [HttpPost]
        public IActionResult Delete(int Id)
        {
            if (ModelState.IsValid)
            {
                if (Id == 0)
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong.");
                    return RedirectToAction(nameof(Index));
                }
                _service.DeleteProject(Id);
            }
            return RedirectToAction(nameof(Index));
        }

        public List<SelectListItem> SetList()
        {
            var list = new List<SelectListItem>();
            var t = _customerService.GetCustomers();
            list.Add(new SelectListItem
            {
                Text = "Select customer",
                Value = 0.ToString()
            });
            foreach (var cust in t)
            {
                list.Add(new SelectListItem
                {
                    Text = cust.Name,
                    Value = cust.Id.ToString()
                });
            }
            return list;
        }
    }
}
