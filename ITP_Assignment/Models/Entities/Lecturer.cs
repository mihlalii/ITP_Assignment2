using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITP_Assignment.Models.Entities
{
    public class Lecturer
    {
        [Key]
        public int LecturerId { get; set; }

        // Navigation
        [NotMapped]
        public ICollection<Course>? Courses { get; set; }
    }
}
