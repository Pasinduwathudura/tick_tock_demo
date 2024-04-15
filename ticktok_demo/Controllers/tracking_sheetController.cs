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
            db.Configuration.ProxyCreationEnabled = false;

            return db.tracking_sheet;
        }


        [HttpGet]
        [Route("~/api/trakingsheet/employee")]
        [ResponseType(typeof(tracking_sheet))]
        public IQueryable<tracking_sheet> GetTrackingSheetEmployee()
        {
            db.Configuration.ProxyCreationEnabled = false;
            Guid currentUserId = new Guid(User.Identity.GetUserId());

            return db.tracking_sheet.Where(t => t.employeeId == currentUserId).Include(p => p.project);


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

            if (id != tracking_sheet.trackingId)
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
                if (tracking_sheetExists(tracking_sheet.trackingId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            // Create a new object with modified property names
            var response = new
            {
                date = tracking_sheet.trackingDate,
                startTime = tracking_sheet.trackingStartTime, // Change property name here
                endTime = tracking_sheet.trackingEndTime,
                //workingHours = "",
                tracking_sheet.projectId,
                tracking_sheet.employeeId, 
                tracking_sheet.trackingId,  
                //tracking_sheet.companyId,  
                tracking_sheet.approveStatus,
                tracking_sheet.dayType
            };

            return CreatedAtRoute("DefaultApi", new { id = tracking_sheet.trackingId }, response);
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
            return db.tracking_sheet.Count(e => e.trackingId == id) > 0;
        }
    }
}