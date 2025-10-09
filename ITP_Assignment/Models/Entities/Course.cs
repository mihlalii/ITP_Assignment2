using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITP_Assignment.Models.Entities
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }

        [Required]
        [StringLength(100)]
        public string CourseTitle { get; set; } = string.Empty;

        [ForeignKey("Lecturer")]
        public int LecturerId { get; set; }

        
        public Lecturer? Lecturer { get; set; } = null!;
        public ICollection<Module>? Modules { get; set; } = new List<Module>();
        public ICollection<CourseStudent> CourseStudents { get; set; }
    }
}
