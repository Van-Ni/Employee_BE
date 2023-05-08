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
        [HttpGet]
        [Route("api/Department/GetAllDepartments")]
        public IHttpActionResult GetAllPositions()
        {
            var departments = db.departments.Select(d => new { id = d.id, name = d.name, description = d.description }).ToList();

            return Ok(departments);
        }

        [HttpGet]
        [Route("api/Department/GetDepartment/{id}")]
        public IHttpActionResult GetDepartment(int id)
        {
            var dep = db.departments.Where(d => d.id == id)
                .Select(d => new
                {
                    id = d.id,
                    name = d.name,
                    description = d.description
                })
                .SingleOrDefault();
            if (dep == null)
            {
                return NotFound();
            }

            return Ok(dep);
        }

        [HttpPost]
        [Route("api/Department/CreateDepartment", Name = "CreateDepartment")]
        public IHttpActionResult CreateDepartment(department department)
        {
            var existingDepartment = db.departments.FirstOrDefault(d => d.name == department.name);
            if (existingDepartment != null)
            {
                return BadRequest("Phòng ban này đã tồn tại.");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.departments.Add(department);
                    db.SaveChanges();
                    return Ok(new { id = department.id, name = department.name, description = department.description });
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
        }

        [HttpDelete]
        [Route("api/Department/DeleteDepartment/{id}")]
        public IHttpActionResult DeleteDepartment(int id)
        {
            var departmentToDelete = db.departments.Find(id);
            if (departmentToDelete == null)
            {
                return NotFound();
            }

            db.departments.Remove(departmentToDelete);
            db.SaveChanges();
            return Ok();
        }

        [HttpPut]
        [Route("api/Department/UpdateDepartment/{id}")]
        public IHttpActionResult UpdateDepartment(int id, department department)
        {
            var depToUpdate = db.departments.Find(id);
            if (depToUpdate == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                depToUpdate.name = department.name;
                depToUpdate.description = department.description;
                db.SaveChanges();
                return Ok(depToUpdate);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
