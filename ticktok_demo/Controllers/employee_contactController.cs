using Microsoft.AspNet.Identity;
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
using System.Web.UI;
using System.Xml.Linq;
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

        [HttpGet]
        [Route("~/api/employee_contact/current/user")]
        public async Task<IHttpActionResult> Getemployeescurrentuser()
        {
            Guid currentUserId = new Guid(User.Identity.GetUserId());
          

            System.Diagnostics.Debug.WriteLine(currentUserId);
            db.Configuration.ProxyCreationEnabled = false;
            var response = db.employee_contact
                .Where(t => t.employee.emp_id == currentUserId)
                .Include(c => c.employee.company)
                .Include(cu => cu.employee.job_description)
                .Include(h => h.employee.holiday)
                .Include(ec => ec.employee)
                .Include(c => c.country)
                .Include(c => c.employee.company.client).SingleOrDefault();
            return Ok(response);

            //var response = db.employee_contact.Where(t => t.employee.emp_id == currentUserId)
            //    .Include(c => c.employee.company).ToList();

            //if (response.Count > 0)
            //{
            //    return Ok(response[0]);
            //}
            //else
            //{
            //    return NotFound();
            //}




            //return Ok(db.employee_contact.Where(t => t.employee.emp_id == currentUserId).Include(c => c.employee.company).Include(cu => cu.employee.job_description).Include(h => h.employee.holiday).Include(ec => ec.employee).Include(c => c.country).Include(c => c.employee.company.client).Include(b => b.employee).ToList());
        }


        //[HttpGet]
        //[Route("~/api/employee_contact/current/user")]
        //public async Task<IHttpActionResult> Getemployeescurrentuser()
        //{
        //    Guid currentUserId = new Guid(User.Identity.GetUserId());
        //    System.Diagnostics.Debug.WriteLine(currentUserId);
        //    db.Configuration.ProxyCreationEnabled = false;
        //    var response = db.employee_contact
        //        .Where(t => t.employee.emp_id == currentUserId)
        //        .Include(c => c.employee.company)
        //        .Include(cu => cu.employee.job_description)
        //        .Include(h => h.employee.holiday)
        //        .Include(ec => ec.employee)
        //        .Include(c => c.country)
        //        .Include(c => c.employee.company.client)
        //        .ToList();

        //    if (response.Any())
        //    {
        //        return Ok(response[0]);
        //    }
        //    else
        //    {
        //        // Handle the case when no records are
        //        Console.WriteLine($"Name:");

        //        return NotFound();
        //    }
        //}

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