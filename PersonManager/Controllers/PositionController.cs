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

        // GET api/Position
        [HttpGet]
        public IHttpActionResult GetPositions()
        {
            var positions = db.positions
                .OrderByDescending(p => p.id)
                .Select(p => new {
                    Id = p.id,
                    Name = p.name,
                    Description = p.description
                })
                .ToList();
            if (positions.Count == 0)
            {
                return NotFound();
            }
            return Ok(positions);
        }
    }
}
