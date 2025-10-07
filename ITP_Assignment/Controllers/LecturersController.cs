using ITP_Assignment.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            var allLecturers=dbContext.Lecturers.ToList();
            return Ok(allLecturers);
        }
        //[HttpPost]

        //public IActionResult AddLecturer()
        //{

        ///}

    }
}
