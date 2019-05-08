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
    public class LiquidTanksController : ApiController
    {
        private FullDBmodel db = new FullDBmodel();

        // GET: api/LiquidTanks
        public IQueryable<LiquidTanks> GetLiquidTanks()
        {
            return db.LiquidTanks;
        }

        // GET: api/LiquidTanks/5
        [ResponseType(typeof(LiquidTanks))]
        public IHttpActionResult GetLiquidTanks(string id)
        {
            LiquidTanks liquidTanks = db.LiquidTanks.Find(id);
            if (liquidTanks == null)
            {
                return NotFound();
            }

            return Ok(liquidTanks);
        }

        // PUT: api/LiquidTanks/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLiquidTanks(string id, LiquidTanks liquidTanks)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != liquidTanks.Name)
            {
                return BadRequest();
            }

            db.Entry(liquidTanks).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LiquidTanksExists(id))
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

        // POST: api/LiquidTanks
        [ResponseType(typeof(LiquidTanks))]
        public IHttpActionResult PostLiquidTanks(LiquidTanks liquidTanks)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.LiquidTanks.Add(liquidTanks);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (LiquidTanksExists(liquidTanks.Name))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = liquidTanks.Name }, liquidTanks);
        }

        // DELETE: api/LiquidTanks/5
        [ResponseType(typeof(LiquidTanks))]
        public IHttpActionResult DeleteLiquidTanks(string id)
        {
            LiquidTanks liquidTanks = db.LiquidTanks.Find(id);
            if (liquidTanks == null)
            {
                return NotFound();
            }

            db.LiquidTanks.Remove(liquidTanks);
            db.SaveChanges();

            return Ok(liquidTanks);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LiquidTanksExists(string id)
        {
            return db.LiquidTanks.Count(e => e.Name == id) > 0;
        }
    }
}