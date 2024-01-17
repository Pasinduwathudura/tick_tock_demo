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
    public class employee_contactController : ApiController
    {
        private tickEntities db = new tickEntities();

        // GET: api/employee_contact
        public IQueryable<employee_contact> Getemployee_contact()
        {
            return db.employee_contact;
        }

        // GET: api/employee_contact/5
        [ResponseType(typeof(employee_contact))]
        public async Task<IHttpActionResult> Getemployee_contact(Guid id)
        {
            employee_contact employee_contact = await db.employee_contact.FindAsync(id);
            if (employee_contact == null)
            {
                return NotFound();
            }

            return Ok(employee_contact);
        }

        // PUT: api/employee_contact/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putemployee_contact(Guid id, employee_contact employee_contact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != employee_contact.emp_cont_id)
            {
                return BadRequest();
            }

            db.Entry(employee_contact).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!employee_contactExists(id))
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

        // POST: api/employee_contact
        [ResponseType(typeof(employee_contact))]
        public async Task<IHttpActionResult> Postemployee_contact(employee_contact employee_contact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.employee_contact.Add(employee_contact);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (employee_contactExists(employee_contact.emp_cont_id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = employee_contact.emp_cont_id }, employee_contact);
        }

        // DELETE: api/employee_contact/5
        [ResponseType(typeof(employee_contact))]
        public async Task<IHttpActionResult> Deleteemployee_contact(Guid id)
        {
            employee_contact employee_contact = await db.employee_contact.FindAsync(id);
            if (employee_contact == null)
            {
                return NotFound();
            }

            db.employee_contact.Remove(employee_contact);
            await db.SaveChangesAsync();

            return Ok(employee_contact);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool employee_contactExists(Guid id)
        {
            return db.employee_contact.Count(e => e.emp_cont_id == id) > 0;
        }
    }
}