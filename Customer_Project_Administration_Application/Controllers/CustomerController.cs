using Customer_Project_Administration_Application.Services;
using Customer_Project_Administration_Application.ViewModels.CustomerFolder;
using Microsoft.AspNetCore.Mvc;

namespace Customer_Project_Administration_Application.Controllers
{
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
                }).ToList();
            return View(model);
        }

        public IActionResult Create()
        {
            var model = new CreateCustomerViewModel();
            model.
                
        }
    }
}
