
using ITP_Assignment.Data;
using ITP_Assignment.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
namespace ITP_Assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskItemController : ControllerBase
    {
        private readonly LecturerContext dbContext;

        public TaskItemController(LecturerContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllTasks()
        {
            var tasks = dbContext.Tasks
                .Include(t => t.Module)
                .Select(static t => new
                {
                    t.TaskItemId,
                    t.TaskName,
                    t.DueDate,
                    t.Status,
                    ModuleName = t.Module.ModuleName
                })
                .ToList();

            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public IActionResult GetTaskById(int id)
        {
            var task = dbContext.Tasks
                .Include(t => t.Module)
                .FirstOrDefault(t => t.TaskItemId == id);

            if (task == null)
                return NotFound($"Task with ID {id} not found.");

            return Ok(task);
        }

        [HttpPost]
        public IActionResult CreateTask([FromBody] TaskItem task)
        {
            if (task == null)
                return BadRequest("Task data is required.");

            
            var module = dbContext.Modules
                .Include(m=>m.Course)
                .FirstOrDefault(m=>m.ModuleId==task.ModuleId);
            if (module == null)
                return BadRequest($"Module with ID {task.ModuleId} not found.");

            task.LecturerId = module.Course.LecturerId;

            
            if (!string.IsNullOrEmpty(task.Status))
                task.Status = "Not Started";

            dbContext.Tasks.Add(task);
            dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetTaskById), new { id = task.TaskItemId }, task);
        }

        
        [HttpPut("{id}")]
        public IActionResult UpdateTask(int id, [FromBody] TaskItem updatedTask)
        {
            var task = dbContext.Tasks.Find(id);
            if (task == null)
                return NotFound($"Task with ID {id} not found.");

            task.TaskName = updatedTask.TaskName;
            task.DueDate = updatedTask.DueDate;
            task.Status = updatedTask.Status;

            dbContext.SaveChanges();
            return Ok(task);
        }

        
        [HttpPut("{id}/status")]
        public IActionResult UpdateTaskStatus(int id, [FromBody] string status)
        {
            var task = dbContext.Tasks.Find(id);
            if (task == null)
                return NotFound($"Task with ID {id} not found.");

            var validStatuses = new[] { "Not Started", "In Progress", "Complete" };
            if (!validStatuses.Contains(status))
                return BadRequest($"Invalid status. Use one of: {string.Join(", ", validStatuses)}");

            task.Status = status;
            dbContext.SaveChanges();

            return Ok($"Task {id} status updated to '{status}'.");
        }

     
        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id)
        {
            var task = dbContext.Tasks.Find(id);
            if (task == null)
                return NotFound($"Task with ID {id} not found.");

            dbContext.Tasks.Remove(task);
            dbContext.SaveChanges();

            return Ok($"Task with ID {id} deleted successfully.");
        }
    }
}








