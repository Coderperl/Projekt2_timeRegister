using System.ComponentModel.DataAnnotations;

namespace Projekt2_tidrapportering.DTOS.CustomerDTOS
{
    public class UpdateCustomerDTO
    {
        [Required]
        public string Name { get; set; }
        
    }
}
