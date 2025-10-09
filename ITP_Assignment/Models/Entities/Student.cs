using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITP_Assignment.Models.Entities
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public User? User { get; set; }

        // Navigation
        public ICollection<CourseStudent>? CourseStudents { get; set; }
    }
}
