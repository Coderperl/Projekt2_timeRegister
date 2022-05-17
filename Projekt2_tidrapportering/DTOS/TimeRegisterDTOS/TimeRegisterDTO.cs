using Projekt2_tidrapportering.DTOS.CustomerDTOS;
using Projekt2_tidrapportering.DTOS.ProjectDTOS;

namespace Projekt2_tidrapportering.DTOS.TimeRegisterDTOS
{
    public class TimeRegisterDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int AmountMinutes { get; set; }
        public string Description { get; set; }
        public ProjectDTO Project { get; set; } = new ProjectDTO();
        //public CustomerDTO Customer { get; set; } = new CustomerDTO();
    }
}
