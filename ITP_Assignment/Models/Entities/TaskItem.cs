using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace ITP_Assignment.Models.Entities
{
    public class TaskItem
    {
        public Guid Id { get; set; }
        public int TaskItemId { get; set; }

        [Required]
        public string TaskName { get; set; } = string.Empty;

        [Required]
        public DateTime DueDate { get; set; }

        [ForeignKey("Module")]
        public int ModuleId { get; set; }

        
        public int Status { get; set; }

      
        public Module? Module { get; set; }
    }
}
