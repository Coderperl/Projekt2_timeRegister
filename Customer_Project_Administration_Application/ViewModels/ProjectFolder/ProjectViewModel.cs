using Projekt2_tidrapportering.DTOS.CustomerDTOS;

namespace Customer_Project_Administration_Application.ViewModels.ProjectFolder
{
    public class ProjectViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CustomerDTO Customer { get; set; }
    }
}
