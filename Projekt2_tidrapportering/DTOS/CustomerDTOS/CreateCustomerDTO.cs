using System.ComponentModel.DataAnnotations;

namespace Projekt2_tidrapportering.DTOS.CustomerDTOS
{
    public class CreateCustomerDTO
    {
        [Required]
        public string Name { get; set; }
       
    }
}
