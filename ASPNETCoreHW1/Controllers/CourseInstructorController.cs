using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ASPNETCoreHW1.Models;

namespace ASPNETCoreHW1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseInstructorController : ControllerBase
    {
        private readonly ContosouniversityContext db;
        public CourseInstructorController(ContosouniversityContext db)
        {
            this.db = db;
        }

        [HttpGet("")]
        public ActionResult<IEnumerable<CourseInstructor>> GetCourseInstructors()
        {
            return db.CourseInstructor.ToList();
        }

        [HttpGet("{CourseID}")]
        public ActionResult<CourseInstructor> GetCourseInstructorById(int CourseID, int InstructorID)
        {
            return db.CourseInstructor.Find(CourseID, InstructorID);
        }

        [HttpPost("")]
        public ActionResult<CourseInstructor> PostCourseInstructor(CourseInstructor model)
        {
            db.CourseInstructor.Add(model);
            db.SaveChanges();

            return Created("/api/CourseInstructor",model);
        }

        [HttpDelete("{CourseID}")]
        public ActionResult<CourseInstructor> DeleteCourseInstructorById(int CourseID, int InstructorID)
        {
            var data = db.CourseInstructor.Find(CourseID, InstructorID);
            db.CourseInstructor.Remove(data);
            db.SaveChanges();
            //db.Database.ExecuteSqlRaw($"Delete from db.CourseInstructor where CourseId={CourseID} and InstructorID={InstructorID}");

            return Ok(data);
        }
    }
}