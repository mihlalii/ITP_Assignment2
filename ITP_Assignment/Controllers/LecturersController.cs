using ITP_Assignment.Data;
using ITP_Assignment.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using System;
using System.ComponentModel;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ITP_Assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LecturersController : ControllerBase
    {
        private readonly LecturerContext dbContext;


        public LecturersController(LecturerContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]

        public IActionResult GetAllLecturers()
        {
            try
            {

                var lecturers = dbContext.Lecturers
                    .Include(l => l.Courses)
                        .ThenInclude(c => c.Modules)
                    .ToList();

                if (!lecturers.Any())
                {
                    return NotFound("No lecturers found in the database.");
                }

                return Ok(lecturers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        [HttpGet("{id}")]
        public IActionResult GetLecturerById(int id)
        {
            var lecturer = dbContext.Lecturers
                .Include(l => l.Courses)
                    .ThenInclude(c => c.Modules)
                .FirstOrDefault(l => l.LecturerId == id);

            if (lecturer == null)
                return NotFound("Lecturer not found.");

            return Ok(lecturer);
        }


        [HttpPost("{lecturerId}/CreateTask")]
        public IActionResult CreateTask(int lecturerId, [FromBody] CreateTaskDto dto)
        {
            if (dto == null)
                return BadRequest("Task data is missing.");

            var lecturer = dbContext.Lecturers
                .Include(l => l.Courses)
                    .ThenInclude(c => c.Modules)
                .FirstOrDefault(l => l.LecturerId == lecturerId);

            if (lecturer == null)
                return NotFound("Lecturer not found.");

            var module = dbContext.Modules
                .Include(m => m.Course)
                .FirstOrDefault(m => m.ModuleId == dto.ModuleId);

            if (module == null)
                return BadRequest("Invalid module ID.");

            if (module.Course.LecturerId != lecturerId)
                return BadRequest("Module does not belong to this lecturer.");

            var newTask = new TaskItem
            {
                TaskName = dto.TaskName,
                DueDate = dto.DueDate,
                ModuleId = dto.ModuleId,
                Status = 0
            };

            dbContext.Tasks.Add(newTask);
            dbContext.SaveChanges();

            return Ok(new
            {
                message = "Task created successfully.",
                newTask.TaskItemId,
                newTask.TaskName,
                newTask.DueDate
            });
        }

    }
}



















        







        


        

        
        

        
        
    

