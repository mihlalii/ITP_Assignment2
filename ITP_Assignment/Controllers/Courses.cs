using ITP_Assignment.Data;
using ITP_Assignment.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ITP_Assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Courses : ControllerBase
    {
        private readonly LecturerContext dbContext;

        public Courses(LecturerContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // ✅ GET all courses (with lecturer and modules)
        [HttpGet]
        public IActionResult GetAllCourses()
        {
            var courses = dbContext.Courses
                .Include(c => c.Lecturer)
                .Include(c => c.Modules)
                .ToList();

            if (!courses.Any())
                return NotFound("No courses found in the system.");

            return Ok(courses);
        }

        // ✅ GET course by ID
        [HttpGet("{id}")]
        public IActionResult GetCourseById(int id)
        {
            var course = dbContext.Courses
                .Include(c => c.Lecturer)
                .Include(c => c.Modules)
                .FirstOrDefault(c => c.CourseId == id);

            if (course == null)
                return NotFound($"Course with ID {id} not found.");

            return Ok(course);
        }

        // ✅ POST add a new course
        [HttpPost]
        public IActionResult CreateCourse([FromBody] CreateCourseDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var lecturer = dbContext.Lecturers.FirstOrDefault(l => l.LecturerId == dto.LecturerId);
            if (lecturer == null)
                return BadRequest($"Lecturer with ID {dto.LecturerId} does not exist.");

            var newCourse = new Course
            {
                CourseTitle = dto.CourseName,
                LecturerId = dto.LecturerId
            };

            dbContext.Courses.Add(newCourse);
            dbContext.SaveChanges();

            return Ok(new
            {
                message = "Course created successfully.",
                newCourse.CourseId,
                newCourse.CourseTitle,
                newCourse.LecturerId
            });
        }

        // ✅ PUT update existing course
        [HttpPut("{id}")]
        public IActionResult UpdateCourse(int id, [FromBody] CreateCourseDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var course = dbContext.Courses.Find(id);
            if (course == null)
                return NotFound($"Course with ID {id} not found.");

            var lecturer = dbContext.Lecturers.FirstOrDefault(l => l.LecturerId == dto.LecturerId);
            if (lecturer == null)
                return BadRequest($"Lecturer with ID {dto.LecturerId} does not exist.");

            course.CourseTitle = dto.CourseName;
            course.LecturerId = dto.LecturerId;

            dbContext.SaveChanges();

            return Ok(new
            {
                message = "Course updated successfully.",
                course.CourseId,
                course.CourseTitle,
                course.LecturerId
            });
        }

        // ✅ DELETE a course
        [HttpDelete("{id}")]
        public IActionResult DeleteCourse(int id)
        {
            var course = dbContext.Courses.Find(id);
            if (course == null)
                return NotFound($"Course with ID {id} not found.");

            dbContext.Courses.Remove(course);
            dbContext.SaveChanges();

            return Ok(new { message = "Course deleted successfully." });
        }
    }
}

