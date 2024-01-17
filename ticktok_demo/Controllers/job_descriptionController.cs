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
    public class job_descriptionController : ApiController
    {
        private tickEntities db = new tickEntities();

        // GET: api/job_description
        public IQueryable<job_description> Getjob_description()
        {
            return db.job_description;
        }

        // GET: api/job_description/5
        [ResponseType(typeof(job_description))]
        public async Task<IHttpActionResult> Getjob_description(Guid id)
        {
            job_description job_description = await db.job_description.FindAsync(id);
            if (job_description == null)
            {
                return NotFound();
            }

            return Ok(job_description);
        }

        // PUT: api/job_description/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putjob_description(Guid id, job_description job_description)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != job_description.job_desc_id)
            {
                return BadRequest();
            }

            db.Entry(job_description).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!job_descriptionExists(id))
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

        // POST: api/job_description
        [ResponseType(typeof(job_description))]
        public async Task<IHttpActionResult> Postjob_description(job_description job_description)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.job_description.Add(job_description);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (job_descriptionExists(job_description.job_desc_id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = job_description.job_desc_id }, job_description);
        }

        // DELETE: api/job_description/5
        [ResponseType(typeof(job_description))]
        public async Task<IHttpActionResult> Deletejob_description(Guid id)
        {
            job_description job_description = await db.job_description.FindAsync(id);
            if (job_description == null)
            {
                return NotFound();
            }

            db.job_description.Remove(job_description);
            await db.SaveChangesAsync();

            return Ok(job_description);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool job_descriptionExists(Guid id)
        {
            return db.job_description.Count(e => e.job_desc_id == id) > 0;
        }
    }
}