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
    public class companiesController : ApiController
    {
        private tickEntities db = new tickEntities();

        // GET: api/companies
        public IQueryable<company> Getcompanies()
        {
            return db.companies;
        }

        // GET: api/companies/5
        [ResponseType(typeof(company))]
        public async Task<IHttpActionResult> Getcompany(Guid id)
        {
            company company = await db.companies.FindAsync(id);
            if (company == null)
            {
                return NotFound();
            }

            return Ok(company);
        }

        // PUT: api/companies/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putcompany(Guid id, company company)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != company.company_id)
            {
                return BadRequest();
            }

            db.Entry(company).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!companyExists(id))
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

        // POST: api/companies
        [ResponseType(typeof(company))]
        public async Task<IHttpActionResult> Postcompany(company company)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.companies.Add(company);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (companyExists(company.company_id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = company.company_id }, company);
        }

        // DELETE: api/companies/5
        [ResponseType(typeof(company))]
        public async Task<IHttpActionResult> Deletecompany(Guid id)
        {
            company company = await db.companies.FindAsync(id);
            if (company == null)
            {
                return NotFound();
            }

            db.companies.Remove(company);
            await db.SaveChangesAsync();

            return Ok(company);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool companyExists(Guid id)
        {
            return db.companies.Count(e => e.company_id == id) > 0;
        }
    }
}