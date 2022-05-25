using System.ComponentModel.DataAnnotations;

namespace Projekt2_tidrapportering.DTOS.CustomerDTOS
{
    public class CustomerDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        
    }
}
