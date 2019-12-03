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
    public class OrderController : ApiController
    {
        private AMSAngularEntities1 db = new AMSAngularEntities1();

        // GET: api/Order
        public List<PurchaseViewModel> GettblPurchases()
        {
            db.Configuration.ProxyCreationEnabled = true;
            List<tblPurchase> purchaseList = db.tblPurchases.Where(x=>x.purchaseStatus== "Asset Registered Internally").ToList();
            List<PurchaseViewModel> pList = purchaseList.Select(x => new PurchaseViewModel
            {
                purchaseId = x.purchaseId,
                purchaseOrderNo = x.purchaseOrderNo,
                assetName = x.tblAssetDefinition.assetName,
                assetType = x.tblAssetType.assetTypeName,
                quantity = Convert.ToDecimal(x.quantity),
                vendorName = x.tblVendor.vendorName,
                purchaseDateStr = x.purchaseDateStr,
                purchaseDelivDateStr = Convert.ToString(x.purchaseDelivDate),
                purchaseStatus = x.purchaseStatus
            }).ToList();
            return pList;
        }

        // GET: api/Order/5
        [ResponseType(typeof(tblPurchase))]
        public PurchaseViewModel GettblPurchase(string ordno)
        {
            db.Configuration.ProxyCreationEnabled = true;
            tblPurchase porder = db.tblPurchases.Where(x => x.purchaseOrderNo == ordno).FirstOrDefault();
            PurchaseViewModel pomodel = new PurchaseViewModel();
            if (porder == null)
            {
              
            }

            else
            {
                pomodel.purchaseId = porder.purchaseId;
                pomodel.purchaseOrderNo = porder.purchaseOrderNo;
                pomodel.assetName = porder.tblAssetDefinition.assetName;
                pomodel.assetType = porder.tblAssetType.assetTypeName;
                pomodel.purchaseAssetName = porder.purchaseAssetName;
                pomodel.purchaseAssetType = porder.purchaseAssetType;
                pomodel.purchaseVendorName = porder.purchaseVendorName;
                pomodel.quantity = Convert.ToDecimal(porder.quantity);
                pomodel.vendorName = porder.tblVendor.vendorName;
                pomodel.purchaseDateStr = porder.purchaseDateStr;
                pomodel.purchaseDelivDateStr = porder.purchaseDelivDateStr;
                pomodel.purchaseStatus = porder.purchaseStatus;
            }
            return pomodel;
        }

        // PUT: api/Order/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PuttblPurchase(int id, tblPurchase tblPurchase)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblPurchase.purchaseId)
            {
                return BadRequest();
            }

            db.Entry(tblPurchase).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblPurchaseExists(id))
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

        // POST: api/Order
        [ResponseType(typeof(tblPurchase))]
        public IHttpActionResult PosttblPurchase(tblPurchase tblPurchase)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tblPurchases.Add(tblPurchase);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tblPurchase.purchaseId }, tblPurchase);
        }

        // DELETE: api/Order/5
        [ResponseType(typeof(tblPurchase))]
        public IHttpActionResult DeletetblPurchase(int id)
        {
            tblPurchase tblPurchase = db.tblPurchases.Find(id);
            if (tblPurchase == null)
            {
                return NotFound();
            }

            db.tblPurchases.Remove(tblPurchase);
            db.SaveChanges();

            return Ok(tblPurchase);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblPurchaseExists(int id)
        {
            return db.tblPurchases.Count(e => e.purchaseId == id) > 0;
        }
    }
}