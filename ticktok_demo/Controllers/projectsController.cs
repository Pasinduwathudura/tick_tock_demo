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
    public class projectsController : ApiController
    {
        private tickEntities db = new tickEntities();

        // GET: api/projects
        public IQueryable<project> Getprojects()
        {
            return db.projects;
        }

        // GET: api/projects/5
        [ResponseType(typeof(project))]
        public async Task<IHttpActionResult> Getproject(Guid id)
        {
            project project = await db.projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            return Ok(project);
        }

        // PUT: api/projects/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putproject(Guid id, project project)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != project.project_id)
            {
                return BadRequest();
            }

            db.Entry(project).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!projectExists(id))
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

        // POST: api/projects
        [ResponseType(typeof(project))]
        public async Task<IHttpActionResult> Postproject(project project)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.projects.Add(project);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (projectExists(project.project_id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = project.project_id }, project);
        }

        // DELETE: api/projects/5
        [ResponseType(typeof(project))]
        public async Task<IHttpActionResult> Deleteproject(Guid id)
        {
            project project = await db.projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            db.projects.Remove(project);
            await db.SaveChangesAsync();

            return Ok(project);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool projectExists(Guid id)
        {
            return db.projects.Count(e => e.project_id == id) > 0;
        }
    }
}