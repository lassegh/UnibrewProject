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
    public class ProcessingItemsController : ApiController
    {
        private FullDBmodel db = new FullDBmodel();

        // GET: api/ProcessingItems
        public IQueryable<ProcessingItems> GetProcessingItems()
        {
            return db.ProcessingItems;
        }

        // GET: api/ProcessingItems/5
        [ResponseType(typeof(ProcessingItems))]
        public IHttpActionResult GetProcessingItems(int id)
        {
            ProcessingItems processingItems = db.ProcessingItems.Find(id);
            if (processingItems == null)
            {
                return NotFound();
            }

            return Ok(processingItems);
        }

        // PUT: api/ProcessingItems/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProcessingItems(string id, ProcessingItems processingItems)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!id.Equals(processingItems.ProcessNumber))
            {
                return BadRequest();
            }

            db.Entry(processingItems).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProcessingItemsExists(id))
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

        // POST: api/ProcessingItems
        [ResponseType(typeof(ProcessingItems))]
        public IHttpActionResult PostProcessingItems(ProcessingItems processingItems)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProcessingItems.Add(processingItems);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ProcessingItemsExists(processingItems.ProcessNumber))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = processingItems.ProcessNumber }, processingItems);
        }

        // DELETE: api/ProcessingItems/5
        [ResponseType(typeof(ProcessingItems))]
        public IHttpActionResult DeleteProcessingItems(int id)
        {
            ProcessingItems processingItems = db.ProcessingItems.Find(id);
            if (processingItems == null)
            {
                return NotFound();
            }

            db.ProcessingItems.Remove(processingItems);
            db.SaveChanges();

            return Ok(processingItems);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProcessingItemsExists(string id)
        {
            return db.ProcessingItems.Count(e => e.ProcessNumber == id) > 0;
        }
    }
}