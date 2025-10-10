using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITP_Assignment.Models.Entities
{
    public class User
    {
        [Key]
        public int UserId { get; set; }   // Matches DB typo intentionally

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Suranme { get; set; } = string.Empty;

        [Required]
        public string Gender { get; set; } = string.Empty;
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        public string Address { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        [ForeignKey("Role")]
        public int RoleID { get; set; }

        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string StaffOrStudetNumber { get; set; } = string.Empty;

        // Navigation
        public Role? Role { get; set; }
        public Student? Student { get; set; }


    }
}
