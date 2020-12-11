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
    public class OfficeAssignmentController : ControllerBase
    {
        private readonly ContosouniversityContext db;
        public OfficeAssignmentController(ContosouniversityContext db)
        {
            this.db = db;
        }

        [HttpGet("")]
        public ActionResult<IEnumerable<OfficeAssignment>> GetOficeAssignments()
        {
            return db.OfficeAssignment.ToList();
        }

        [HttpGet("{InstructorID}")]
        public ActionResult<OfficeAssignment> GetOficeAssignmentById(int InstructorID)
        {
            return db.OfficeAssignment.Find(InstructorID);
        }

        [HttpPost("")]
        public ActionResult<OfficeAssignment> PostOficeAssignment(OfficeAssignment model)
        {
            db.OfficeAssignment.Add(model);
            db.SaveChanges();

            return Created("/api/OfficeAssignment/"+model.InstructorId,model);
        }

        [HttpPut("{InstructorID}")]
        public IActionResult PutOficeAssignment(int InstructorID, OfficeAssignment model)
        {
            var data = db.OfficeAssignment.Find(InstructorID);

            data.InjectFrom(model);

            db.OfficeAssignment.Update(data);
            db.SaveChanges();
            
            return NoContent();
        }

        [HttpDelete("{InstructorID}")]
        public ActionResult<OfficeAssignment> DeleteOficeAssignmentById(int InstructorID)
        {
            var data = db.OfficeAssignment.Find(InstructorID);
            db.OfficeAssignment.Remove(data);
            db.SaveChanges();
            //db.Database.ExecuteSqlRaw($"Delete from db.OfficeAssignment where InstructorID={InstructorID}");

            return Ok(data);
        }
    }
}