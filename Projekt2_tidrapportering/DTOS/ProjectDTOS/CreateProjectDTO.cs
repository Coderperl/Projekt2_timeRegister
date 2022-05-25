using System.ComponentModel.DataAnnotations;

namespace Projekt2_tidrapportering.DTOS.ProjectDTOS
{
    public class CreateProjectDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int CustomerId { get; set; }

    }
}
