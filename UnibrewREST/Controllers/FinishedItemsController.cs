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
using UnibrewREST.Models;

namespace UnibrewREST.Controllers
{
    public class FinishedItemsController : ApiController
    {
        private Quayzer db = new Quayzer();

        // GET: api/FinishedItems
        public IQueryable<FinishedItem> GetFinishedItems()
        {
            return db.FinishedItems;
        }

        // GET: api/FinishedItems/5
        [ResponseType(typeof(FinishedItem))]
        public IHttpActionResult GetFinishedItem(int id)
        {
            FinishedItem finishedItem = db.FinishedItems.Find(id);
            if (finishedItem == null)
            {
                return NotFound();
            }

            return Ok(finishedItem);
        }

        // PUT: api/FinishedItems/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFinishedItem(int id, FinishedItem finishedItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != finishedItem.FinishedItemNumber)
            {
                return BadRequest();
            }

            db.Entry(finishedItem).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FinishedItemExists(id))
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
        [ResponseType(typeof(FinishedItem))]
        public IHttpActionResult PostFinishedItem(FinishedItem finishedItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.FinishedItems.Add(finishedItem);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (FinishedItemExists(finishedItem.FinishedItemNumber))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = finishedItem.FinishedItemNumber }, finishedItem);
        }

        // DELETE: api/FinishedItems/5
        [ResponseType(typeof(FinishedItem))]
        public IHttpActionResult DeleteFinishedItem(int id)
        {
            FinishedItem finishedItem = db.FinishedItems.Find(id);
            if (finishedItem == null)
            {
                return NotFound();
            }

            db.FinishedItems.Remove(finishedItem);
            db.SaveChanges();

            return Ok(finishedItem);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FinishedItemExists(int id)
        {
            return db.FinishedItems.Count(e => e.FinishedItemNumber == id) > 0;
        }
    }
}