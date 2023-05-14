using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PersonManager.Models;
namespace PersonManager.Controllers
{
    public class RoleController : ApiController
    {
        private HRMEntities db = new HRMEntities();

        // GET api/roles
        [HttpGet]
        [Route("api/Role/GetRoles")]
        public IHttpActionResult GetRoles()
        {
            var roles = db.roles
                .Select(e => new {
                Id = e.id, Name = e.name,Description = e.description
                })
                .ToList();
            if(roles.Count == 0)
            {
                return NotFound();
            }
            return Ok(roles);
        }

        // GET api/roles/{id}
        [HttpGet]
        [Route("api/Role/GetRole/{id}")]
        public IHttpActionResult GetRole(int id)
        {
            var role = db.roles.Where(e => e.id == id)
                .Select(e => new
                {
                    Id = e.id,
                    Name = e.name,
                    Description = e.description
                })
                .SingleOrDefault();
            if (role == null)
            {
                return NotFound();
            }

            return Ok(role);
        }

        // POST api/roles
        [HttpPost]
        public IHttpActionResult CreateRole(role role)
        {
            if (ModelState.IsValid)
            {
                db.roles.Add(role);
                db.SaveChanges();
                return CreatedAtRoute("DefaultApi", new { id = role.id }, role);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // PUT api/roles/{id}
        [HttpPut]
        [Route("api/Role/UpdateRole/{id}")]
        public IHttpActionResult UpdateRole(int id,role role)
        {
            var roleToUpdate = db.roles.Find(id);
            if (roleToUpdate == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                roleToUpdate.name = role.name;
                roleToUpdate.description = role.description;
                db.SaveChanges();
                return Ok(roleToUpdate);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // DELETE api/roles/{id}
        [HttpDelete]
        [Route("api/Role/DeleteRole/{id}")]
        public IHttpActionResult Delete(int id)
        {
            var roleToDelete = db.roles.Find(id);
            if (roleToDelete == null)
            {
                return NotFound();
            }

            db.roles.Remove(roleToDelete);
            db.SaveChanges();
            return Ok();
        }
    }
}
