using System.ComponentModel.DataAnnotations;

namespace ITP_Assignment.Models.Entities
{
    public class CreateTaskDto
    {
        public string TaskName { get; set; } = string.Empty;
 
        [DataType(DataType.Date)]
   

        public DateTime DueDate { get; set; }
        public int ModuleId { get; set; }

        public required string Status { get; set; }
    }
}
