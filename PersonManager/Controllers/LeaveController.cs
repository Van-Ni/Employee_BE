using PersonManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PersonManager.Controllers
{
    public class LeaveController : ApiController
    {
        private HRMEntities db = new HRMEntities();
        // GET api/leaves
        [HttpGet]
        [Route("api/Leave/Gets")]
        public IHttpActionResult Gets()
        {
            var leaves = db.leaves
                .Select(e => new
                {
                    Id = e.id,
                    LeaveDate = e.leave_date,
                    Reason = e.reason,
                    Approved = e.approved,
                    LeaveType = e.leave_type,
                    EmployeeId = e.employee_id
                })
                .ToList();

            if (leaves.Count == 0)
            {
                return NotFound();
            }

            return Ok(leaves);
        }

        // GET api/leaves/{id}
        [HttpGet]
        [Route("api/Leave/GetLeave/{id}")]
        public IHttpActionResult GetLeave(int id)
        {
            var leave = db.leaves.Where(e => e.id == id)
                .Select(e => new
                {
                    Id = e.id,
                    LeaveDate = e.leave_date,
                    Reason = e.reason,
                    Approved = e.approved,
                    LeaveType = e.leave_type,
                    EmployeeId = e.employee_id
                })
                .SingleOrDefault();

            if (leave == null)
            {
                return NotFound();
            }

            return Ok(leave);
        }
        // POST api/leaves
        [HttpPost]
        public IHttpActionResult Create(leaf leave)
        {
            if (ModelState.IsValid)
            {
                db.leaves.Add(leave);
                db.SaveChanges();
                return CreatedAtRoute("DefaultApi", new { id = leave.id }, leave);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        // PUT api/leaves/{id}
        [HttpPut]
        [Route("api/Leave/UpdateLeave/{id}")]
        public IHttpActionResult UpdateLeave(int id, leaf leave)
        {
            var leaveToUpdate = db.leaves.Find(id);
            if (leaveToUpdate == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                leaveToUpdate.leave_date = leave.leave_date;
                leaveToUpdate.reason = leave.reason;
                leaveToUpdate.approved = leave.approved;
                leaveToUpdate.leave_type = leave.leave_type;
                leaveToUpdate.employee_id = leave.employee_id;

                db.SaveChanges();
                return Ok(leaveToUpdate);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        // DELETE api/leaves/{id}
        [HttpDelete]
        [Route("api/Leave/DeleteLeave/{id}")]
        public IHttpActionResult DeleteLeave(int id)
        {
            var leaveToDelete = db.leaves.Find(id);
            if (leaveToDelete == null)
            {
                return NotFound();
            }

            db.leaves.Remove(leaveToDelete);
            db.SaveChanges();
            return Ok();
        }
    }
}
