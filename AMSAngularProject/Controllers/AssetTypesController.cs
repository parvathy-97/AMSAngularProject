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
using AMSAngularProject.Models;

namespace AMSAngularProject.Controllers
{
    public class AssetTypesController : ApiController
    {
        private AMSAngularEntities1 db = new AMSAngularEntities1();

        public AssetTypesController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }

        // GET: api/AssetTypes
        public IQueryable<tblAssetType> GettblAssetTypes()
        {
            return db.tblAssetTypes;
        }

        // GET: api/AssetTypes/5
        [ResponseType(typeof(tblAssetType))]
        public IHttpActionResult GettblAssetType(int id)
        {
            tblAssetType tblAssetType = db.tblAssetTypes.Find(id);
            if (tblAssetType == null)
            {
                return NotFound();
            }

            return Ok(tblAssetType);
        }

        // PUT: api/AssetTypes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PuttblAssetType(int id, tblAssetType tblAssetType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblAssetType.assetTypeId)
            {
                return BadRequest();
            }

            db.Entry(tblAssetType).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblAssetTypeExists(id))
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

        // POST: api/AssetTypes
        [ResponseType(typeof(tblAssetType))]
        public IHttpActionResult PosttblAssetType(tblAssetType tblAssetType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tblAssetTypes.Add(tblAssetType);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tblAssetType.assetTypeId }, tblAssetType);
        }

        // DELETE: api/AssetTypes/5
        [ResponseType(typeof(tblAssetType))]
        public IHttpActionResult DeletetblAssetType(int id)
        {
            tblAssetType tblAssetType = db.tblAssetTypes.Find(id);
            if (tblAssetType == null)
            {
                return NotFound();
            }

            db.tblAssetTypes.Remove(tblAssetType);
            db.SaveChanges();

            return Ok(tblAssetType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblAssetTypeExists(int id)
        {
            return db.tblAssetTypes.Count(e => e.assetTypeId == id) > 0;
        }
    }
}