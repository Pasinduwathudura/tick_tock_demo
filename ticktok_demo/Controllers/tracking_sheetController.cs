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
    public class tracking_sheetController : ApiController
    {
        private tickEntities db = new tickEntities();

        // GET: api/tracking_sheet
        public IQueryable<tracking_sheet> Gettracking_sheet()
        {
            return db.tracking_sheet;
        }

        // GET: api/tracking_sheet/5
        [ResponseType(typeof(tracking_sheet))]
        public async Task<IHttpActionResult> Gettracking_sheet(Guid id)
        {
            tracking_sheet tracking_sheet = await db.tracking_sheet.FindAsync(id);
            if (tracking_sheet == null)
            {
                return NotFound();
            }

            return Ok(tracking_sheet);
        }

        // PUT: api/tracking_sheet/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Puttracking_sheet(Guid id, tracking_sheet tracking_sheet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tracking_sheet.tracking_id)
            {
                return BadRequest();
            }

            db.Entry(tracking_sheet).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tracking_sheetExists(id))
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

        // POST: api/tracking_sheet
        [ResponseType(typeof(tracking_sheet))]
        public async Task<IHttpActionResult> Posttracking_sheet(tracking_sheet tracking_sheet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tracking_sheet.Add(tracking_sheet);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (tracking_sheetExists(tracking_sheet.tracking_id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = tracking_sheet.tracking_id }, tracking_sheet);
        }

        // DELETE: api/tracking_sheet/5
        [ResponseType(typeof(tracking_sheet))]
        public async Task<IHttpActionResult> Deletetracking_sheet(Guid id)
        {
            tracking_sheet tracking_sheet = await db.tracking_sheet.FindAsync(id);
            if (tracking_sheet == null)
            {
                return NotFound();
            }

            db.tracking_sheet.Remove(tracking_sheet);
            await db.SaveChangesAsync();

            return Ok(tracking_sheet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tracking_sheetExists(Guid id)
        {
            return db.tracking_sheet.Count(e => e.tracking_id == id) > 0;
        }
    }
}