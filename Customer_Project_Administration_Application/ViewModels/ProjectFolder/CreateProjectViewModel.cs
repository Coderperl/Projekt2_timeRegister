using Microsoft.AspNetCore.Mvc.Rendering;

namespace Customer_Project_Administration_Application.ViewModels.ProjectFolder
{
    public class CreateProjectViewModel
    {
        public string Name { get; set; }
        public int CustomerId { get; set; }
        public List<SelectListItem> Customers { get; set; }
    }
}
