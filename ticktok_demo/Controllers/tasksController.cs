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
    public class tasksController : ApiController
    {
        private tickEntities db = new tickEntities();

        // GET: api/tasks
        public IQueryable<task> Gettasks()
        {
            return db.tasks;
        }

        // GET: api/tasks/5
        [ResponseType(typeof(task))]
        public async Task<IHttpActionResult> Gettask(Guid id)
        {
            task task = await db.tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        // PUT: api/tasks/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Puttask(Guid id, task task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != task.taskId)
            {
                return BadRequest();
            }

            db.Entry(task).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!taskExists(id))
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

        [HttpGet]
        [Route("~/api/trakingsheet/task/{id}")]
        [ResponseType(typeof(task))]
        public IQueryable<task> GetTrackingSheetTask(Guid id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            return db.tasks.Where(t => t.trackingSheetId == id).Include(p => p.tracking_sheet.project);
        }

        // POST: api/tasks
        [ResponseType(typeof(task))]
        public async Task<IHttpActionResult> Posttask(task task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tasks.Add(task);
            await db.SaveChangesAsync();

            var responseTask = new
            {
                taskId = task.taskId,
                taskName = task.taskName,
                taskStartTime = task.taskStartTime,
                taskEndTime = task.taskEndTime,
                taskDescription = task.taskDescription,
                trackingSheetId = task.trackingSheetId,
                projectId = task.projectId,
                taskDate = task.taskDate
            };

            return CreatedAtRoute("DefaultApi", new { id = task.taskId }, responseTask);
        }


        // DELETE: api/tasks/5
        [ResponseType(typeof(task))]
        public async Task<IHttpActionResult> Deletetask(Guid id)
        {
            task task = await db.tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            db.tasks.Remove(task);
            await db.SaveChangesAsync();

            return Ok(task);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool taskExists(Guid id)
        {
            return db.tasks.Count(e => e.taskId == id) > 0;
        }
    }
}