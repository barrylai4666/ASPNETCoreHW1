using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ASPNETCoreHW1.Models;
using Omu.ValueInjecter;

namespace ASPNETCoreHW1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly ContosouniversityContext db;
        public EnrollmentController(ContosouniversityContext db)
        {
            this.db = db;
        }

        [HttpGet("")]
        public ActionResult<IEnumerable<Enrollment>> GetEnrollments()
        {
            return db.Enrollment.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Enrollment> GetEnrollmentById(int EnrollmentId)
        {
             return db.Enrollment.Find(EnrollmentId);
        }

        [HttpPost("")]
        public ActionResult<Enrollment> PostEnrollment(Enrollment model)
        {
            db.Enrollment.Add(model);
            db.SaveChanges();

            return Created("/api/Enrollment/"+model.EnrollmentId,model);
        }

        [HttpPut("{EnrollmentID}")]
        public IActionResult PutEnrollment(int EnrollmentID, Enrollment model)
        {
            var data = db.Enrollment.Find(EnrollmentID);

            data.InjectFrom(model);

            db.Enrollment.Update(data);
            db.SaveChanges();
            
            return NoContent();
        }

        [HttpDelete("{EnrollmentID}")]
        public ActionResult<Enrollment> DeleteEnrollmentById(int EnrollmentID)
        {
            var data = db.Enrollment.Find(EnrollmentID);
            db.Enrollment.Remove(data);
            db.SaveChanges();
            //db.Database.ExecuteSqlRaw($"Delete from db.Enrollment where EnrollmentID={EnrollmentID}");

            return Ok(data);
        }
    }
}