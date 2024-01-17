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
    public class leave_employeeController : ApiController
    {
        private tickEntities db = new tickEntities();

        // GET: api/leave_employee
        public IQueryable<leave_employee> Getleave_employee()
        {
            return db.leave_employee;
        }

        // GET: api/leave_employee/5
        [ResponseType(typeof(leave_employee))]
        public async Task<IHttpActionResult> Getleave_employee(Guid id)
        {
            leave_employee leave_employee = await db.leave_employee.FindAsync(id);
            if (leave_employee == null)
            {
                return NotFound();
            }

            return Ok(leave_employee);
        }

        // PUT: api/leave_employee/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putleave_employee(Guid id, leave_employee leave_employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != leave_employee.leave_emp_id)
            {
                return BadRequest();
            }

            db.Entry(leave_employee).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!leave_employeeExists(id))
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

        // POST: api/leave_employee
        [ResponseType(typeof(leave_employee))]
        public async Task<IHttpActionResult> Postleave_employee(leave_employee leave_employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.leave_employee.Add(leave_employee);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (leave_employeeExists(leave_employee.leave_emp_id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = leave_employee.leave_emp_id }, leave_employee);
        }

        // DELETE: api/leave_employee/5
        [ResponseType(typeof(leave_employee))]
        public async Task<IHttpActionResult> Deleteleave_employee(Guid id)
        {
            leave_employee leave_employee = await db.leave_employee.FindAsync(id);
            if (leave_employee == null)
            {
                return NotFound();
            }

            db.leave_employee.Remove(leave_employee);
            await db.SaveChangesAsync();

            return Ok(leave_employee);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool leave_employeeExists(Guid id)
        {
            return db.leave_employee.Count(e => e.leave_emp_id == id) > 0;
        }
    }
}