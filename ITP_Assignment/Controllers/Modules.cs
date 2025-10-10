using ITP_Assignment.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ITP_Assignment.Models.Entities;
using Microsoft.EntityFrameworkCore;
namespace ITP_Assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Modules : ControllerBase
    {
        private readonly LecturerContext dbContext;

        public Modules(LecturerContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // ✅ GET all modules
        [HttpGet]
        public IActionResult GetAllModules()
        {
            var modules = dbContext.Modules
                .Include(m => m.Course)
                .ThenInclude(c => c.Lecturer)
                .ToList();

            if (!modules.Any())
                return NotFound("No modules found in the system.");

            return Ok(modules);
        }

        // ✅ GET module by ID
        [HttpGet("{id}")]
        public IActionResult GetModuleById(int id)
        {
            var module = dbContext.Modules
                .Include(m => m.Course)
                .ThenInclude(c => c.Lecturer)
                .FirstOrDefault(m => m.ModuleId == id);

            if (module == null)
                return NotFound($"Module with ID {id} not found.");

            return Ok(module);
        }

        // ✅ POST add new module
        [HttpPost]
        public IActionResult CreateModule([FromBody] CreateModuleDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Ensure Course exists
            var course = dbContext.Courses.FirstOrDefault(c => c.CourseId == dto.CourseId);
            if (course == null)
                return BadRequest($"Course with ID {dto.CourseId} does not exist.");

            var module = new Module
            {
                ModuleName = dto.ModuleName,
                CourseId = dto.CourseId
            };

            dbContext.Modules.Add(module);
            dbContext.SaveChanges();

            return Ok(new
            {
                message = "Module added successfully.",
                module.ModuleId,
                module.ModuleName,
                module.CourseId
            });
        }

        // ✅ PUT update module
        [HttpPut("{id}")]
        public IActionResult UpdateModule(int id, [FromBody] CreateModuleDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var module = dbContext.Modules.Find(id);
            if (module == null)
                return NotFound($"Module with ID {id} not found.");

            var course = dbContext.Courses.FirstOrDefault(c => c.CourseId == dto.CourseId);
            if (course == null)
                return BadRequest($"Course with ID {dto.CourseId} does not exist.");

            module.ModuleName = dto.ModuleName;
            module.CourseId = dto.CourseId;

            dbContext.SaveChanges();

            return Ok(new { message = "Module updated successfully.", module });
        }

        // ✅ DELETE module
        [HttpDelete("{id}")]
        public IActionResult DeleteModule(int id)
        {
            var module = dbContext.Modules.Find(id);
            if (module == null)
                return NotFound($"Module with ID {id} not found.");

            dbContext.Modules.Remove(module);
            dbContext.SaveChanges();

            return Ok(new { message = "Module deleted successfully." });
        }
    }

}
