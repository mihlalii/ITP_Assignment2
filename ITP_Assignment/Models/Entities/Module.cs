using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITP_Assignment.Models.Entities
{
    public class Module
    {
        [Key]
        public int ModuleId { get; set; }

        [Required]
        [StringLength(100)]
        public string ModuleName { get; set; } = string.Empty;

        [ForeignKey("Course")]
        public int CourseId { get; set; }

        
        public Course? Course { get; set; }
        public ICollection<TaskItem>? Tasks { get; set; } = null!;
    }
}
