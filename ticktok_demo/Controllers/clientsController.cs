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
    public class clientsController : ApiController
    {
        private tickEntities db = new tickEntities();

        // GET: api/clients
        public IQueryable<client> Getclients()
        {
            return db.clients;
        }

        // GET: api/clients/5
        [ResponseType(typeof(client))]
        public async Task<IHttpActionResult> Getclient(Guid id)
        {
            client client = await db.clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }

            return Ok(client);
        }

        // PUT: api/clients/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putclient(Guid id, client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != client.client_id)
            {
                return BadRequest();
            }

            db.Entry(client).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!clientExists(id))
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

        // POST: api/clients
        [ResponseType(typeof(client))]
        public async Task<IHttpActionResult> Postclient(client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.clients.Add(client);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (clientExists(client.client_id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = client.client_id }, client);
        }

        // DELETE: api/clients/5
        [ResponseType(typeof(client))]
        public async Task<IHttpActionResult> Deleteclient(Guid id)
        {
            client client = await db.clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }

            db.clients.Remove(client);
            await db.SaveChangesAsync();

            return Ok(client);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool clientExists(Guid id)
        {
            return db.clients.Count(e => e.client_id == id) > 0;
        }
    }
}