using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PersonManager.Models;

namespace PersonManager.Controllers
{
    public class ContractController : ApiController
    {
        private HRMEntities db = new HRMEntities();
        [HttpGet]
        [Route("api/Contract/GetAllContracts")]
        public IHttpActionResult GetAllContracts()
        {
            var contracts = db.contracts.Select(c => new { id = c.id, type = c.type, startDate = c.startDate, endDate = c.endDate, note = c.note }).ToList();

            return Ok(contracts);
        }

        [HttpGet]
        [Route("api/Contract/GetContract/{id}")]
        public IHttpActionResult GetContract(int id)
        {
            var con = db.contracts.Where(c => c.id == id)
                .Select(c => new
                {
                    id = c.id,
                    type = c.type,
                    startDate = c.startDate,
                    endDate = c.endDate,
                    note = c.note
                })
                .SingleOrDefault();
            if (con == null)
            {
                return NotFound();
            }

            return Ok(con);
        }

        [HttpPost]
        [Route("api/Contract/CreateContract", Name = "CreateContract")]
        public IHttpActionResult CreateContract(contract contract)
        {
            if (ModelState.IsValid)
            {
                db.contracts.Add(contract);
                db.SaveChanges();
                return Ok(new { id = contract.id, type = contract.type, startDate = contract.startDate, endDate = contract.endDate, note = contract.note });
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpDelete]
        [Route("api/Contract/DeleteContract/{id}")]
        public IHttpActionResult DeleteContract(int id)
        {
            var contractToDelete = db.contracts.Find(id);
            if (contractToDelete == null)
            {
                return NotFound();
            }

            db.contracts.Remove(contractToDelete);
            db.SaveChanges();
            return Ok();
        }

        [HttpPut]
        [Route("api/Contract/UpdateContract/{id}")]
        public IHttpActionResult UpdateContract(int id, contract contract)
        {
            var conToUpdate = db.contracts.Find(id);
            if (conToUpdate == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                conToUpdate.type = contract.type;
                conToUpdate.startDate = contract.startDate;
                conToUpdate.endDate = contract.endDate;
                conToUpdate.note = contract.note;
                db.SaveChanges();
                return Ok(conToUpdate);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
