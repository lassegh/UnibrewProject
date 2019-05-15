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
    public class LiquidTanksController : ApiController
    {
        private Quayzer db = new Quayzer();

        // GET: api/LiquidTanks
        public IQueryable<LiquidTank> GetLiquidTanks()
        {
            return db.LiquidTanks;
        }

        // GET: api/LiquidTanks/5
        [ResponseType(typeof(LiquidTank))]
        public IHttpActionResult GetLiquidTank(string id)
        {
            LiquidTank liquidTank = db.LiquidTanks.Find(id);
            if (liquidTank == null)
            {
                return NotFound();
            }

            return Ok(liquidTank);
        }

        // PUT: api/LiquidTanks/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLiquidTank(string id, LiquidTank liquidTank)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != liquidTank.Name)
            {
                return BadRequest();
            }

            db.Entry(liquidTank).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LiquidTankExists(id))
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
        [ResponseType(typeof(LiquidTank))]
        public IHttpActionResult PostLiquidTank(LiquidTank liquidTank)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.LiquidTanks.Add(liquidTank);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (LiquidTankExists(liquidTank.Name))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = liquidTank.Name }, liquidTank);
        }

        // DELETE: api/LiquidTanks/5
        [ResponseType(typeof(LiquidTank))]
        public IHttpActionResult DeleteLiquidTank(string id)
        {
            LiquidTank liquidTank = db.LiquidTanks.Find(id);
            if (liquidTank == null)
            {
                return NotFound();
            }

            db.LiquidTanks.Remove(liquidTank);
            db.SaveChanges();

            return Ok(liquidTank);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LiquidTankExists(string id)
        {
            return db.LiquidTanks.Count(e => e.Name == id) > 0;
        }
    }
}