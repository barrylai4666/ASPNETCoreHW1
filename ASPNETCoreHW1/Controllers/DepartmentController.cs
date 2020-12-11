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
    public class DepartmentController : ControllerBase
    {
        private readonly ContosouniversityContext db;
        private readonly ContosouniversityContextProcedures sp;
        public DepartmentController(ContosouniversityContext db, ContosouniversityContextProcedures sp)
        {
            this.sp = sp;
            this.db = db;
        }

        [HttpGet("")]
        public ActionResult<IEnumerable<Department>> GetDepartments()
        {
            return db.Department.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Department> GetDepartmentById(int id)
        {
            return db.Department.Find(id); ;
        }

        [HttpPost("")]
        public ActionResult<Department> PostDepartment(Department model)
        {
            db.Department.Add(model);
            db.SaveChanges();

            return Created("/api/Department/" + model.DepartmentId, model);
        }

        [HttpPost("~/apiPostBySP/[controller]")]
        public ActionResult<Department> PostDepartmentBySP(Department model)
        {

            // {
            //   "name": "應用開發部",
            //   "budget": 2000,
            //   "startDate": "2020-12-09T10:24:28.041Z",
            //   "instructorId": 30,
            //   "rowVersion": "0x00000000000007D5"
            // }
            //var result = sp.Department_Insert(model.Name, model.Budget, model.StartDate, model.InstructorId).GetAwaiter().GetResult();
            var result = sp.Department_Insert(model.Name, model.Budget, model.StartDate, model.InstructorId, null);

            return Created("/api/Department", result);
        }

      
        [HttpPut("{id}")]
        public IActionResult PutDepartment(int id, Department model)
        {
            var data = db.Department.Find(id);
            model.DateModified = DateTime.Now;
            data.InjectFrom(model);

            db.Department.Update(data);
            db.SaveChanges();

            return NoContent();
            
        }

          [HttpPut("~/apiPutBySP/[controller]")]
        public IActionResult PutDepartmentBySP(int id, Department model)
        {
            var result = sp.Department_Update(id, model.Name, model.Budget, model.StartDate, model.InstructorId, null);

            return NoContent();
        }


        [HttpDelete("{id}")]
        public ActionResult<Department> DeleteDepartmentById(int id)
        {
            var data = db.Department.Find(id);
            data.IsDeleted = true;
            db.Department.Update(data);
            db.SaveChanges();
            //db.Database.ExecuteSqlRaw($"Delete from db.Department where DepartmentID={id}");

            return Ok(data);
        }

        [HttpDelete("~/apiDeleteBySP/[controller]")]
        public ActionResult<Department> DeleteDepartmentBySP(int id)
        {
            var data = db.Department.Find(id);
           
            var result = sp.Department_Delete(id, data.RowVersion);

            return Ok(data);
        }

        [HttpGet("DepartmentCourseCount")]
        public ActionResult<IEnumerable<VwDepartmentCourseCount>> GetDepartmentCourseCount()
        {
            return db.VwDepartmentCourseCount.FromSqlInterpolated($"select * from [dbo].[vwDepartmentCourseCount] with(nolock)").ToList();
        }
    }
}