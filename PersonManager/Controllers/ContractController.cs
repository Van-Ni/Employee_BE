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

        // GET api/Contract
        [HttpGet]
        public IHttpActionResult GetContracts()
        {
            var contracts = db.contracts
                .OrderByDescending(c => c.id)
                .Select(c => new {
                    Id = c.id,
                    Type = c.type,
                    StartDate = c.startDate,
                    EndDate = c.endDate,
                    Note = c.note
                })
                .ToList();
            if (contracts.Count == 0)
            {
                return NotFound();
            }
            return Ok(contracts);
        }
    }
}
