using System.ComponentModel.DataAnnotations.Schema;

namespace ITP_Assignment.Models.Entities
{
    public class CourseStudent
    {
        [ForeignKey("Course")]
        public int CourseId { get; set; }

        [ForeignKey("Student")]
        public int StudentId { get; set; }

        public Course? Course { get; set; }
        public Student? Student { get; set; }
    }
}
