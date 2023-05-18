using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PersonManager.Models;

namespace PersonManager.Controllers
{
    public class EmployeeController : ApiController
    {
        private HRMEntities db = new HRMEntities();
        [HttpGet]
        public IHttpActionResult GetEmployees()
        {
            var employees = db.employees
                .Include("user")
                .Include("department")
                .OrderByDescending(e => e.id)
                .Select(e => new {
                    Id = e.id,
                    Fullname = e.fullname,
                    Gender = e.gender,
                    Email = e.email,
                    Phone = e.phone,
                    Address = e.address,
                    Birthday = e.birthday,
                    JoinDate = e.joindate,
                    Avatar = e.avatar,
                    Status = e.status,
                    DepartmentId = e.department_id,
                    DepartmentName = e.department != null ? e.department.name : null,
                    PositionId = e.position_id,
                    PositionName = e.position != null ? e.position.name : null,
                    ContractId = e.contract_Id,
                    ContractName = e.contract != null ? e.contract.type : null,
                    UserId = e.user_id,
                    UserName= e.user != null ? e.user.username : null
                })
                .ToList();
            if (employees.Count == 0)
            {
                return NotFound();
            }
            return Ok(employees);
        }
        [HttpGet]
        [Route("api/Employee/GetEmployee/{id}")]
        public IHttpActionResult GetEmployee(int id)
        {
            var emp = db.employees.Where(e => e.id == id)
                .Select(e => new
                {
                    Id = e.id,
                    Fullname = e.fullname,
                    Gender = e.gender,
                    Email = e.email,
                    Phone = e.phone,
                    Address = e.address,
                    Birthday = e.birthday,
                    JoinDate = e.joindate,
                    Avatar = e.avatar,
                    Status = e.status,
                    DepartmentId = e.department_id,
                    DepartmentName = e.department != null ? e.department.name : null,
                    PositionId = e.position_id,
                    PositionName = e.position != null ? e.position.name : null,
                    ContractId = e.contract_Id,
                    ContractName = e.contract != null ? e.contract.type : null,
                    UserId = e.user_id,
                    UserName = e.user != null ? e.user.username : null
                })
                .SingleOrDefault();
            if (emp == null)
            {
                return NotFound();
            }

            return Ok(emp);
        }
        // POST api/employees
        [HttpPost]
        public IHttpActionResult CreateEmployee(employee employee)
        {
            if (ModelState.IsValid)
            {
                db.employees.Add(employee);
                db.SaveChanges();
                return CreatedAtRoute("DefaultApi", new { id = employee.id }, employee);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        [HttpPut]
        [Route("api/Employee/UpdateEmployee/{id}")]
        public IHttpActionResult UpdateEmployee(int id, employee employee)
        {
            var empToUpdate = db.employees.Find(id);
            if (empToUpdate == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                empToUpdate.fullname = employee.fullname;
                empToUpdate.gender = employee.gender;
                empToUpdate.email = employee.email;
                empToUpdate.phone = employee.phone;
                empToUpdate.address = employee.address;
                empToUpdate.birthday = employee.birthday;
                empToUpdate.joindate = employee.joindate;
                empToUpdate.status = employee.status;
                empToUpdate.department_id = employee.department_id;
                empToUpdate.position_id = employee.position_id;
                empToUpdate.contract_Id = employee.contract_Id;
                db.SaveChanges();
                return Ok(empToUpdate);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        // DELETE api/roles/{id}
        [HttpDelete]
        [Route("api/Employee/DeleteEmployee/{id}")]
        public IHttpActionResult DeleteEmployee(int id)
        {
            var empToDelete = db.employees.Find(id);
            if (empToDelete == null)
            {
                return NotFound();
            }

            db.employees.Remove(empToDelete);
            db.SaveChanges();
            return Ok();
        }
    }
}
