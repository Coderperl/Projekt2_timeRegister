using Customer_Project_Administration_Application.Services;
using Customer_Project_Administration_Application.ViewModels.CustomerFolder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projekt2_tidrapportering.DTOS.CustomerDTOS;

namespace Customer_Project_Administration_Application.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        
        public IActionResult Index() 
        {
            var model = new CustomersViewModel();
            model.Customers = _customerService.GetCustomers()
                .Select(c => new CustomersViewModel.CustomerItemViewModel
                {
                    Id = c.Id,
                    Name = c.Name
                    //baksdlaskdmlasd
                }).ToList();
            return View(model);
        }
        
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateCustomerDTO customer)
        {
            if (ModelState.IsValid)
            {
                var result = _customerService.CreateCustomers(customer);
                if (result == Status.Error)
                {
                    ModelState.AddModelError(customer.Name, "Something went wrong");
                    return RedirectToAction(nameof(Create));
                }
                return RedirectToAction(nameof(Index));
            }
            
            return View();
        }
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var cust = _customerService.GetCustomers().First(c => c.Id == Id);
            var model = new EditCustomerViewModel();
            model.Name = cust.Name;
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(UpdateCustomerDTO customer, int Id)
        {
            if (ModelState.IsValid)
            {
                _customerService.GetCustomers().First(c => c.Id == Id);
                if (Id == 0)
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong.");
                    return View();
                }
                var result = _customerService.UpdateCustomers(customer, Id);
                if (result == Status.Error)
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong");
                    return View();
                }
            }
            return View();
        }

        [HttpPost]
        public IActionResult Delete(int Id)
        {
            if (!ModelState.IsValid)
            {
                if (Id == 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            _customerService.DeleteCustomers(Id);
            return RedirectToAction(nameof(Index));

        }


    }
}
