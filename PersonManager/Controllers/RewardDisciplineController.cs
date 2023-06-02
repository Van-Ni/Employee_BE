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
    public class RewardDisciplineController : ApiController
    {
        private HRMEntities db = new HRMEntities();

        // GET api/rewarddiscipline
        [HttpGet]
        [Route("api/RewardDiscipline/Gets")]
        public IHttpActionResult Gets()
        {
            var transactions = db.rewardDisciplines
                .Select(e => new {
                    Id = e.id,
                    TransactionDate = e.transaction_date,
                    RewardAmount = e.reward_amount,
                    Reason = e.reason,
                    EmployeeId = e.employee_id
                })
                .ToList();

            if (transactions.Count == 0)
            {
                return NotFound();
            }

            return Ok(transactions);
        }
        //

        // GET api/rewarddiscipline/{id}
        [HttpGet]
        [Route("api/RewardDiscipline/Get/{id}")]
        public IHttpActionResult Get(int id)
        {
            var transaction = db.rewardDisciplines.Where(e => e.id == id)
                .Select(e => new {
                    Id = e.id,
                    TransactionDate = e.transaction_date,
                    RewardAmount = e.reward_amount,
                    Reason = e.reason,
                    EmployeeId = e.employee_id
                })
                .SingleOrDefault();

            if (transaction == null)
            {
                return NotFound();
            }

            return Ok(transaction);
        }
        // POST api/rewarddiscipline
        [HttpPost]
        public IHttpActionResult Create(rewardDiscipline transaction)
        {
            if (ModelState.IsValid)
            {
                db.rewardDisciplines.Add(transaction);
                db.SaveChanges();
                return CreatedAtRoute("DefaultApi", new { id = transaction.id }, transaction);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // PUT api/rewarddisciplines/{id}
        [HttpPut]
        [Route("api/RewardDiscipline/UpdateRewardDiscipline/{id}")]
        public IHttpActionResult UpdateRewardDiscipline(int id, rewardDiscipline rewardDiscipline)
        {
            var rewardDisciplineToUpdate = db.rewardDisciplines.Find(id);
            if (rewardDisciplineToUpdate == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                rewardDisciplineToUpdate.transaction_date = rewardDiscipline.transaction_date;
                rewardDisciplineToUpdate.reward_amount = rewardDiscipline.reward_amount;
                rewardDisciplineToUpdate.reason = rewardDiscipline.reason;
                rewardDisciplineToUpdate.employee_id = rewardDiscipline.employee_id;
                db.SaveChanges();
                return Ok(rewardDisciplineToUpdate);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // DELETE api/rewarddiscipline/{id}
        [HttpDelete]
        [Route("api/RewardDiscipline/Delete/{id}")]
        public IHttpActionResult Delete(int id)
        {
            var transactionToDelete = db.rewardDisciplines.Find(id);
            if (transactionToDelete == null)
            {
                return NotFound();
            }

            db.rewardDisciplines.Remove(transactionToDelete);
            db.SaveChanges();

            return Ok();
        }
    }


}
