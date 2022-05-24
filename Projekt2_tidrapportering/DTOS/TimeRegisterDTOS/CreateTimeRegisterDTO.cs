using System.ComponentModel.DataAnnotations;
using Projekt2_tidrapportering.DTOS.CustomerDTOS;
using Projekt2_tidrapportering.DTOS.ProjectDTOS;

namespace Projekt2_tidrapportering.DTOS.TimeRegisterDTOS
{
    public class CreateTimeRegisterDTO
    {   [Required]
        public DateTime Date { get; set; }
        [Required]
        [Range(30,int.MaxValue)]
        public int AmountMinutes { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int ProjectId { get; set; }
        [Required]
        public int CustomerId { get; set; }
    }
}
