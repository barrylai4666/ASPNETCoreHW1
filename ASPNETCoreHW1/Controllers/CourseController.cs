using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ASPNETCoreHW1.Models;
using Omu.ValueInjecter;
using Microsoft.EntityFrameworkCore;

namespace ASPNETCoreHW1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ContosouniversityContext db;
        public CourseController(ContosouniversityContext db)
        {
            this.db = db;
        }

        [HttpGet("")]
        public ActionResult<IEnumerable<Course>> GetCourses()
        {
            return db.Course.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Course> GetCourseById(int id)
        {
            return db.Course.Find(id);
        }

        [HttpPost("")]
        public ActionResult<Course> PostCourse(Course model)
        {
            db.Course.Add(model);
            db.SaveChanges();

            return Created("/api/Course/"+model.CourseId,model);
        }

        [HttpPut("{id}")]
        public IActionResult PutCourse(int id, Course model)
        {
            var data = db.Course.Find(id);
            model.DateModified = DateTime.Now;
            data.InjectFrom(model);

            db.Course.Update(data);
            db.SaveChanges();
            
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult<Course> DeleteCourseById(int id)
        {
            var data = db.Course.Find(id);
            data.IsDeleted = true;
            db.Course.Update(data);
            db.SaveChanges();
            //db.Database.ExecuteSqlRaw($"Delete from db.Course where CourseId={id}");

            return Ok(data);
        }

        [HttpGet("CourseStudent")]
        public ActionResult<IEnumerable<VwCourseStudents>> GetCourseStudents()
        {
            return db.VwCourseStudents.ToList();
        }

        [HttpGet("CourseStudentCount")]
        public ActionResult<IEnumerable<VwCourseStudentCount>> GetCourseStudentCount()
        {
            return db.VwCourseStudentCount.ToList();
        }
    }
}