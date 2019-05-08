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
    public class FinishedItemsController : ApiController
    {
        private FullDBmodel db = new FullDBmodel();

        // GET: api/FinishedItems
        public IQueryable<FinishedItems> GetFinishedItems()
        {
            return db.FinishedItems;
        }

        // GET: api/FinishedItems/5
        [ResponseType(typeof(FinishedItems))]
        public IHttpActionResult GetFinishedItems(int id)
        {
            FinishedItems finishedItems = db.FinishedItems.Find(id);
            if (finishedItems == null)
            {
                return NotFound();
            }

            return Ok(finishedItems);
        }

        // PUT: api/FinishedItems/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFinishedItems(int id, FinishedItems finishedItems)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != finishedItems.FinishedItemNumber)
            {
                return BadRequest();
            }

            db.Entry(finishedItems).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FinishedItemsExists(id))
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

        // POST: api/FinishedItems
        [ResponseType(typeof(FinishedItems))]
        public IHttpActionResult PostFinishedItems(FinishedItems finishedItems)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.FinishedItems.Add(finishedItems);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (FinishedItemsExists(finishedItems.FinishedItemNumber))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = finishedItems.FinishedItemNumber }, finishedItems);
        }

        // DELETE: api/FinishedItems/5
        [ResponseType(typeof(FinishedItems))]
        public IHttpActionResult DeleteFinishedItems(int id)
        {
            FinishedItems finishedItems = db.FinishedItems.Find(id);
            if (finishedItems == null)
            {
                return NotFound();
            }

            db.FinishedItems.Remove(finishedItems);
            db.SaveChanges();

            return Ok(finishedItems);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FinishedItemsExists(int id)
        {
            return db.FinishedItems.Count(e => e.FinishedItemNumber == id) > 0;
        }
    }
}