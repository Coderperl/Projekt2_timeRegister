using Projekt2_tidrapportering.DTOS.CustomerDTOS;

namespace Customer_Project_Administration_Application.ViewModels.ProjectFolder
{
    public class ProjectsViewModel
    {
        public List<ProjectItemViewModel> Projects { get; set; }
        public class ProjectItemViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public CustomerDTO Customer { get; set; }
        }
        
    }
}
