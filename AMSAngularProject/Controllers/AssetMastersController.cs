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
    public class AssetMastersController : ApiController
    {
        private AMSAngularEntities1 db = new AMSAngularEntities1();
        AssetMastersController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }

        // GET: api/AssetMasters
        static decimal count;
        public List<MasterViewModel> GettblassetMaster()
        {
            db.Configuration.ProxyCreationEnabled = true;
            List<tblAssetMaster> amList = db.tblAssetMasters.ToList();
            List<MasterViewModel> amvList = amList.Select(x => new MasterViewModel
            {
                assetMasterId = x.assetMasterId,
                assetName = x.assetName,
                assetDefName = x.tblAssetDefinition.assetName,
                assetType = x.assetType,
                assetTypeName = x.tblAssetType.assetTypeName,
                warrantyFrom = x.warrantyFrom,
                warrantyTo = x.warrantyTo,
                make = x.make,
                makeName = x.tblVendor.vendorName,
                model = x.model,
                makeYear = x.makeYearStr,
                purchaseDate = x.purchaseDateStr,
                serialnumber = x.serialnumber,
                warranty = x.warranty

            }).ToList();
            return amvList;
        }

        // GET: api/AssetMasters/5
        [ResponseType(typeof(tblAssetMaster))]
        public IHttpActionResult GettblAssetMaster(int id)
        {
            tblAssetMaster tblAssetMaster = db.tblAssetMasters.Find(id);
            if (tblAssetMaster == null)
            {
                return NotFound();
            }

            return Ok(tblAssetMaster);
        }

        // PUT: api/AssetMasters/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PuttblAssetMaster(int id, tblPurchase tblPurchase)
        {
            count = Convert.ToDecimal(tblPurchase.quantity);
            db.Entry(tblPurchase).State = EntityState.Modified;
            db.SaveChanges();
            return StatusCode(HttpStatusCode.NoContent);

            
        }

        static int min = 1000;
        static int max = 9999;

        // POST: api/AssetMasters
        [ResponseType(typeof(tblAssetMaster))]
        public IHttpActionResult PosttblAssetMaster(tblAssetMaster tblAssetMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            for(int i = 0; i < count; i++)
            {
                
                Random rdm = new Random();
                int id = rdm.Next(min++, max++);
                tblAssetMaster.serialnumber = id.ToString();
                db.tblAssetMasters.Add(tblAssetMaster);
                db.SaveChanges();

            }
            

            return CreatedAtRoute("DefaultApi", new { id = tblAssetMaster.assetMasterId }, tblAssetMaster);
        }

        // DELETE: api/AssetMasters/5
        [ResponseType(typeof(tblAssetMaster))]
        public IHttpActionResult DeletetblAssetMaster(int id)
        {
            tblAssetMaster tblAssetMaster = db.tblAssetMasters.Find(id);
            if (tblAssetMaster == null)
            {
                return NotFound();
            }

            db.tblAssetMasters.Remove(tblAssetMaster);
            db.SaveChanges();

            return Ok(tblAssetMaster);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblAssetMasterExists(int id)
        {
            return db.tblAssetMasters.Count(e => e.assetMasterId == id) > 0;
        }
    }
}