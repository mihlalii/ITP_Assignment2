using System.ComponentModel.DataAnnotations;

namespace ITP_Assignment.Models.Entities
{
    public class CreateCourseDto
    {
        [Required]
        [StringLength(100)]
        public string CourseName { get; set; } = string.Empty;

        [Required]
        public int LecturerId { get; set; }
    }
}
