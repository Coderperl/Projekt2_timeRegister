using Projekt2_tidrapportering.DTOS.CustomerDTOS;

namespace Projekt2_tidrapportering.DTOS.ProjectDTOS
{
    public class ProjectsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CustomerDTO Customer { get; set; }
    }
}
