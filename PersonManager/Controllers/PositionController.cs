using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PersonManager.Models;
using System.Data.Entity;
namespace PersonManager.Controllers
{
    public class PositionController : ApiController
    {
        private HRMEntities db = new HRMEntities();
        [HttpGet]
        [Route("api/Position/GetAllPositions")]
        public IHttpActionResult GetAllPositions()
        {
            var positions = db.positions.Select(p => new { id = p.id, name = p.name, description = p.description }).ToList();

            return Ok(positions);
        }

        [HttpGet]
        [Route("api/Position/GetPosition/{id}")]
        public IHttpActionResult GetPosition(int id)
        {
            var pos = db.positions.Where(p => p.id == id)
                .Select(p => new
                {
                    id = p.id,
                    name = p.name,
                    description = p.description
                })
                .SingleOrDefault();
            if (pos == null)
            {
                return NotFound();
            }

            return Ok(pos);
        }

        [HttpPost]
        [Route("api/Position/CreatePosition", Name = "CreatePosition")]
        public IHttpActionResult CreatePosition(position position)
        {
            var existingPosition = db.positions.FirstOrDefault(p => p.name == position.name);
            if (existingPosition != null)
            {
                return BadRequest("Chức vụ này đã tồn tại.");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.positions.Add(position);
                    db.SaveChanges();
                    return Ok(new { id = position.id, name = position.name, description = position.description });
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
        }

        [HttpDelete]
        [Route("api/Position/DeletePosition/{id}")]
        public IHttpActionResult DeletePosition(int id)
        {
            var positionToDelete = db.positions.Find(id);
            if (positionToDelete == null)
            {
                return NotFound();
            }

            db.positions.Remove(positionToDelete);
            db.SaveChanges();
            return Ok();
        }

        [HttpPut]
        [Route("api/Position/UpdatePosition/{id}")]
        public IHttpActionResult UpdatePosition(int id, position position)
        {
            var posToUpdate = db.positions.Find(id);
            if (posToUpdate == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                posToUpdate.name = position.name;
                posToUpdate.description = position.description;
                db.SaveChanges();
                return Ok(posToUpdate);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }


    }
}
