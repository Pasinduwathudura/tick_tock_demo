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
    public class leave_groupController : ApiController
    {
        private tickEntities db = new tickEntities();

        // GET: api/leave_group
        public IQueryable<leave_group> Getleave_group()
        {
            return db.leave_group;
        }

        // GET: api/leave_group/5
        [ResponseType(typeof(leave_group))]
        public async Task<IHttpActionResult> Getleave_group(Guid id)
        {
            leave_group leave_group = await db.leave_group.FindAsync(id);
            if (leave_group == null)
            {
                return NotFound();
            }

            return Ok(leave_group);
        }

        // PUT: api/leave_group/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putleave_group(Guid id, leave_group leave_group)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != leave_group.leave_id)
            {
                return BadRequest();
            }

            db.Entry(leave_group).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!leave_groupExists(id))
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

        // POST: api/leave_group
        [ResponseType(typeof(leave_group))]
        public async Task<IHttpActionResult> Postleave_group(leave_group leave_group)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.leave_group.Add(leave_group);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (leave_groupExists(leave_group.leave_id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = leave_group.leave_id }, leave_group);
        }

        // DELETE: api/leave_group/5
        [ResponseType(typeof(leave_group))]
        public async Task<IHttpActionResult> Deleteleave_group(Guid id)
        {
            leave_group leave_group = await db.leave_group.FindAsync(id);
            if (leave_group == null)
            {
                return NotFound();
            }

            db.leave_group.Remove(leave_group);
            await db.SaveChangesAsync();

            return Ok(leave_group);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool leave_groupExists(Guid id)
        {
            return db.leave_group.Count(e => e.leave_id == id) > 0;
        }
    }
}