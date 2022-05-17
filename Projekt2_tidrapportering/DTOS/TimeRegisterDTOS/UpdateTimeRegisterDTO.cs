using Projekt2_tidrapportering.DTOS.CustomerDTOS;
using Projekt2_tidrapportering.DTOS.ProjectDTOS;

namespace Projekt2_tidrapportering.DTOS.TimeRegisterDTOS
{
    public class UpdateTimeRegisterDTO
    {
       
        public DateTime Date { get; set; }
        public int AmountMinutes { get; set; }
        public string Description { get; set; }
        public int ProjectId { get; set; } 
        public int CustomerId { get; set; } 
    }
}
