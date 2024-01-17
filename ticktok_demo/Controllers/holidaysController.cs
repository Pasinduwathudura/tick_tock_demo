using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ticktok_demo;

namespace ticktok_demo.Controllers
{
    [Authorize]
    public class holidaysController : ApiController
    {
        private tickEntities db = new tickEntities();

        // GET: api/holidays
        public IQueryable<holiday> Getholidays()
        {
            return db.holidays;
        }

        // GET: api/holidays/5
        [ResponseType(typeof(holiday))]
        public async Task<IHttpActionResult> Getholiday(Guid id)
        {
            holiday holiday = await db.holidays.FindAsync(id);
            if (holiday == null)
            {
                return NotFound();
            }

            return Ok(holiday);
        }

        // PUT: api/holidays/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putholiday(Guid id, holiday holiday)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != holiday.holiday_id)
            {
                return BadRequest();
            }

            db.Entry(holiday).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!holidayExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/holidays
        [ResponseType(typeof(holiday))]
        public async Task<IHttpActionResult> Postholiday(holiday holiday)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.holidays.Add(holiday);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (holidayExists(holiday.holiday_id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = holiday.holiday_id }, holiday);
        }

        // DELETE: api/holidays/5
        [ResponseType(typeof(holiday))]
        public async Task<IHttpActionResult> Deleteholiday(Guid id)
        {
            holiday holiday = await db.holidays.FindAsync(id);
            if (holiday == null)
            {
                return NotFound();
            }

            db.holidays.Remove(holiday);
            await db.SaveChangesAsync();

            return Ok(holiday);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool holidayExists(Guid id)
        {
            return db.holidays.Count(e => e.holiday_id == id) > 0;
        }
    }
}