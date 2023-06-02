using PersonManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PersonManager.Controllers
{
    public class HolidayController : ApiController
    {
        private HRMEntities db = new HRMEntities();

        // GET api/holidays
        [HttpGet]
        [Route("api/Holiday/Gets")]
        public IHttpActionResult Gets()
        {
            var holidays = db.holidays
                .Select(e => new
                {
                    Id = e.id,
                    HolidayDate = e.holiday_date,
                    Description = e.description
                })
                .ToList();

            if (holidays.Count == 0)
            {
                return NotFound();
            }

            return Ok(holidays);
        }

        // GET api/holidays/{id}
        [HttpGet]
        [Route("api/Holiday/GetHoliday/{id}")]
        public IHttpActionResult GetHoliday(int id)
        {
            var holiday = db.holidays.Where(e => e.id == id)
                .Select(e => new
                {
                    Id = e.id,
                    HolidayDate = e.holiday_date,
                    Description = e.description
                })
                .SingleOrDefault();

            if (holiday == null)
            {
                return NotFound();
            }

            return Ok(holiday);
        }

        // POST api/holidays
        [HttpPost]
        public IHttpActionResult CreateHoliday(holiday holiday)
        {
            if (ModelState.IsValid)
            {
                db.holidays.Add(holiday);
                db.SaveChanges();
                return CreatedAtRoute("DefaultApi", new { id = holiday.id }, holiday);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // PUT api/holidays/{id}
        [HttpPut]
        [Route("api/Holiday/UpdateHoliday/{id}")]
        public IHttpActionResult UpdateHoliday(int id, holiday holiday)
        {
            var holidayToUpdate = db.holidays.Find(id);
            if (holidayToUpdate == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                holidayToUpdate.holiday_date = holiday.holiday_date;
                holidayToUpdate.description = holiday.description;

                db.SaveChanges();
                return Ok(holidayToUpdate);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // DELETE api/holidays/{id}
        [HttpDelete]
        [Route("api/Holiday/DeleteHoliday/{id}")]
        public IHttpActionResult DeleteHoliday(int id)
        {
            var holidayToDelete = db.holidays.Find(id);
            if (holidayToDelete == null)
            {
                return NotFound();
            }

            db.holidays.Remove(holidayToDelete);
            db.SaveChanges();
            return Ok();
        }
    }

}
