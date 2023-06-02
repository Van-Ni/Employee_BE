using PersonManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PersonManager.Controllers
{
    public class SalaryController : ApiController
    {
        private HRMEntities db = new HRMEntities();

        // GET api/salaries
        [HttpGet]
        [Route("api/Salary/GetSalaries")]
        public IHttpActionResult GetSalaries()
        {
            var salaries = db.salarys
                .Select(e => new
                {
                    Id = e.id,
                    BasicSalary = e.basicSalary,
                    Allowance = e.allowance,
                    Coefficient = e.coefficient,
                    HourlyRate = e.hourlyRate,
                    EmployeeId = e.employee_id
                })
                .ToList();

            if (salaries.Count == 0)
            {
                return NotFound();
            }

            return Ok(salaries);
        }

        // GET api/salaries/{id}
        [HttpGet]
        [Route("api/Salary/GetSalary/{id}")]
        public IHttpActionResult GetSalary(int id)
        {
            var salary = db.salarys.Where(e => e.id == id)
                .Select(e => new
                {
                    Id = e.id,
                    BasicSalary = e.basicSalary,
                    Allowance = e.allowance,
                    Coefficient = e.coefficient,
                    HourlyRate = e.hourlyRate,
                    EmployeeId = e.employee_id
                })
                .SingleOrDefault();

            if (salary == null)
            {
                return NotFound();
            }

            return Ok(salary);
        }
        // POST api/salaries
        [HttpPost]
        public IHttpActionResult CreateSalary(salary salary)
        {
            if (ModelState.IsValid)
            {
                db.salarys.Add(salary);
                db.SaveChanges();
                return CreatedAtRoute("DefaultApi", new { id = salary.id }, salary);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        // PUT api/salaries/{id}
        [HttpPut]
        [Route("api/Salary/UpdateSalary/{id}")]
        public IHttpActionResult UpdateSalary(int id, salary salary)
        {
            var salaryToUpdate = db.salarys.Find(id);
            if (salaryToUpdate == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                salaryToUpdate.basicSalary = salary.basicSalary;
                salaryToUpdate.allowance = salary.allowance;
                salaryToUpdate.coefficient = salary.coefficient;
                salaryToUpdate.hourlyRate = salary.hourlyRate;
                salaryToUpdate.employee_id = salary.employee_id;

                db.SaveChanges();
                return Ok(salaryToUpdate);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        // DELETE api/salaries/{id}
        [HttpDelete]
        [Route("api/Salary/DeleteSalary/{id}")]
        public IHttpActionResult DeleteSalary(int id)
        {
            var salaryToDelete = db.salarys.Find(id);
            if (salaryToDelete == null)
            {
                return NotFound();
            }

            db.salarys.Remove(salaryToDelete);
            db.SaveChanges();
            return Ok();
        }
    }
}
