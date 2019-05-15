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
    public class TapOperatorsController : ApiController
    {
        private FullDBmodel db = new FullDBmodel();

        // GET: api/TapOperators
        public IQueryable<TapOperator> GetTapOperator()
        {
            return db.TapOperator;
        }

        // GET: api/TapOperators/5
        [ResponseType(typeof(TapOperator))]
        public IHttpActionResult GetTapOperator(int id)
        {
            TapOperator tapOperator = db.TapOperator.Find(id);
            if (tapOperator == null)
            {
                return NotFound();
            }

            return Ok(tapOperator);
        }

        // PUT: api/TapOperators/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTapOperator(int id, TapOperator tapOperator)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tapOperator.ID)
            {
                return BadRequest();
            }

            db.Entry(tapOperator).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TapOperatorExists(id))
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

        // POST: api/TapOperators
        [ResponseType(typeof(TapOperator))]
        public IHttpActionResult PostTapOperator(TapOperator tapOperator)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TapOperator.Add(tapOperator);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tapOperator.ID }, tapOperator);
        }

        // DELETE: api/TapOperators/5
        [ResponseType(typeof(TapOperator))]
        public IHttpActionResult DeleteTapOperator(int id)
        {
            TapOperator tapOperator = db.TapOperator.Find(id);
            if (tapOperator == null)
            {
                return NotFound();
            }

            db.TapOperator.Remove(tapOperator);
            db.SaveChanges();

            return Ok(tapOperator);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TapOperatorExists(int id)
        {
            return db.TapOperator.Count(e => e.ID == id) > 0;
        }
    }
}