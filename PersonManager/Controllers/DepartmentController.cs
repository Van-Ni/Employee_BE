using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PersonManager.Models;

namespace PersonManager.Controllers
{
    public class DepartmentController : ApiController
    {
        private HRMEntities db = new HRMEntities();

        // GET api/Department
        [HttpGet]
        public IHttpActionResult GetDepartments()
        {
            var departments = db.departments
                .OrderByDescending(d => d.id)
                .Select(d => new {
                    Id = d.id,
                    Name = d.name,
                    Description = d.description
                })
                .ToList();
            if (departments.Count == 0)
            {
                return NotFound();
            }
            return Ok(departments);
        }
    }
}
