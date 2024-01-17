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
    public class countriesController : ApiController
    {
        private tickEntities db = new tickEntities();

        // GET: api/countries
        public IQueryable<country> Getcountries()
        {
            return db.countries;
        }

        // GET: api/countries/5
        [ResponseType(typeof(country))]
        public async Task<IHttpActionResult> Getcountry(Guid id)
        {
            country country = await db.countries.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }

            return Ok(country);
        }

        // PUT: api/countries/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putcountry(Guid id, country country)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != country.country_id)
            {
                return BadRequest();
            }

            db.Entry(country).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!countryExists(id))
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

        // POST: api/countries
        [ResponseType(typeof(country))]
        public async Task<IHttpActionResult> Postcountry(country country)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.countries.Add(country);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (countryExists(country.country_id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = country.country_id }, country);
        }

        // DELETE: api/countries/5
        [ResponseType(typeof(country))]
        public async Task<IHttpActionResult> Deletecountry(Guid id)
        {
            country country = await db.countries.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }

            db.countries.Remove(country);
            await db.SaveChangesAsync();

            return Ok(country);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool countryExists(Guid id)
        {
            return db.countries.Count(e => e.country_id == id) > 0;
        }
    }
}