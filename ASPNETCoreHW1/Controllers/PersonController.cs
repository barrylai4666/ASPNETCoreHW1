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
    public class PersonController : ControllerBase
    {
        private readonly ContosouniversityContext db;
        public PersonController(ContosouniversityContext db)
        {
            this.db = db;
        }

        [HttpGet("")]
        public ActionResult<IEnumerable<Person>> GetPersons()
        {
            return db.Person.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Person> GetPersonById(int id)
        {
            return db.Person.Find(id);;
        }

        [HttpPost("")]
        public ActionResult<Person> PostPerson(Person model)
        {
            db.Person.Add(model);
            db.SaveChanges();

            return Created("/api/Person/"+model.Id,model);
        }

        [HttpPut("{id}")]
        public IActionResult PutPerson(int id, Person model)
        {
            var data = db.Person.Find(id);
            model.DateModified = DateTime.Now;
            data.InjectFrom(model);

            db.Person.Update(data);
            db.SaveChanges();
            
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult<Person> DeletePersonById(int id)
        {
            var data = db.Person.Find(id);
            data.IsDeleted = true;
            db.Person.Update(data);
            db.SaveChanges();
            //db.Database.ExecuteSqlRaw($"Delete from db.Person where ID={id}");

            return Ok(data);
        }
    }
}