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
    public class ProcessingItemsController : ApiController
    {
        private Quayzer db = new Quayzer();

        // GET: api/ProcessingItems
        public IQueryable<ProcessingItem> GetProcessingItems()
        {
            return db.ProcessingItems;
        }

        // GET: api/ProcessingItems/5
        [ResponseType(typeof(ProcessingItem))]
        public IHttpActionResult GetProcessingItem(string id)
        {
            ProcessingItem processingItem = db.ProcessingItems.Find(id);
            if (processingItem == null)
            {
                return NotFound();
            }

            return Ok(processingItem);
        }

        // PUT: api/ProcessingItems/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProcessingItem(string id, ProcessingItem processingItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != processingItem.ProcessNumber)
            {
                return BadRequest();
            }

            db.Entry(processingItem).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProcessingItemExists(id))
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
        [ResponseType(typeof(ProcessingItem))]
        public IHttpActionResult PostProcessingItem(ProcessingItem processingItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProcessingItems.Add(processingItem);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ProcessingItemExists(processingItem.ProcessNumber))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = processingItem.ProcessNumber }, processingItem);
        }

        // DELETE: api/ProcessingItems/5
        [ResponseType(typeof(ProcessingItem))]
        public IHttpActionResult DeleteProcessingItem(string id)
        {
            ProcessingItem processingItem = db.ProcessingItems.Find(id);
            if (processingItem == null)
            {
                return NotFound();
            }

            db.ProcessingItems.Remove(processingItem);
            db.SaveChanges();

            return Ok(processingItem);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProcessingItemExists(string id)
        {
            return db.ProcessingItems.Count(e => e.ProcessNumber == id) > 0;
        }
    }
}