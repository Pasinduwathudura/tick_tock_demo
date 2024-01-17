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
    public class calendersController : ApiController
    {
        private tickEntities db = new tickEntities();

        // GET: api/calenders
        public IQueryable<calender> Getcalenders()
        {
            return db.calenders;
        }

        // GET: api/calenders/5
        [ResponseType(typeof(calender))]
        public async Task<IHttpActionResult> Getcalender(Guid id)
        {
            calender calender = await db.calenders.FindAsync(id);
            if (calender == null)
            {
                return NotFound();
            }

            return Ok(calender);
        }

        // PUT: api/calenders/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putcalender(Guid id, calender calender)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != calender.calender_id)
            {
                return BadRequest();
            }

            db.Entry(calender).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!calenderExists(id))
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

        // POST: api/calenders
        [ResponseType(typeof(calender))]
        public async Task<IHttpActionResult> Postcalender(calender calender)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.calenders.Add(calender);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (calenderExists(calender.calender_id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = calender.calender_id }, calender);
        }

        // DELETE: api/calenders/5
        [ResponseType(typeof(calender))]
        public async Task<IHttpActionResult> Deletecalender(Guid id)
        {
            calender calender = await db.calenders.FindAsync(id);
            if (calender == null)
            {
                return NotFound();
            }

            db.calenders.Remove(calender);
            await db.SaveChangesAsync();

            return Ok(calender);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool calenderExists(Guid id)
        {
            return db.calenders.Count(e => e.calender_id == id) > 0;
        }
    }
}