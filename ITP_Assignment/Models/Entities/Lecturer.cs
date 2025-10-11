using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITP_Assignment.Models.Entities
{
    public class Lecturer
    {
        [Key]
        public int LecturerId { get; set; }
        [Required]
        public required string Name { get; set; }
        
        public required string Email { get; set; }
        [NotMapped]
        public ICollection<Course>? Courses { get; set; }
    }
}
