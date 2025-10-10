using System.ComponentModel.DataAnnotations;

namespace ITP_Assignment.Models.Entities
{
    public class CreateModuleDto
    {
        [Required]
        [StringLength(100)]
        public string ModuleName { get; set; } = string.Empty;

        [Required]
        public int CourseId { get; set; }
    }
}
