using ITP_Assignment.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ITP_Assignment.Data
{
    public class LecturerContext:DbContext
    {
        public LecturerContext(DbContextOptions<LecturerContext> options): base(options) 
        {
            
        }

        public DbSet<Lecturer>Lecturers { get; set; }
    }
}
