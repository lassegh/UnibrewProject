using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using UnibrewREST;

namespace UnibrewREST.Controllers
{
    public class TESTmomentsController : ApiController
    {
        private TESTmomentContext db = new TESTmomentContext();

        // GET: api/TESTmoments
        public IQueryable<TESTmoment> GetTESTmoment()
        {
            return db.TESTmoment;
        }

        // GET: api/TESTmoments/5
        [ResponseType(typeof(TESTmoment))]
        public IHttpActionResult GetTESTmoment(int id)
        {
            TESTmoment tESTmoment = db.TESTmoment.Find(id);
            if (tESTmoment == null)
            {
                return NotFound();
            }

            return Ok(tESTmoment);
        }

        // PUT: api/TESTmoments/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTESTmoment(int id, TESTmoment tESTmoment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tESTmoment.Id)
            {
                return BadRequest();
            }

            db.Entry(tESTmoment).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TESTmomentExists(id))
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

        // POST: api/TESTmoments
        [ResponseType(typeof(TESTmoment))]
        public IHttpActionResult PostTESTmoment(TESTmoment tESTmoment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TESTmoment.Add(tESTmoment);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (TESTmomentExists(tESTmoment.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = tESTmoment.Id }, tESTmoment);
        }

        // DELETE: api/TESTmoments/5
        [ResponseType(typeof(TESTmoment))]
        public IHttpActionResult DeleteTESTmoment(int id)
        {
            TESTmoment tESTmoment = db.TESTmoment.Find(id);
            if (tESTmoment == null)
            {
                return NotFound();
            }

            db.TESTmoment.Remove(tESTmoment);
            db.SaveChanges();

            return Ok(tESTmoment);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TESTmomentExists(int id)
        {
            return db.TESTmoment.Count(e => e.Id == id) > 0;
        }
    }
}