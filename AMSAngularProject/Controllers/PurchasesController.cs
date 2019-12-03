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
    public class PurchasesController : ApiController
    {
        private AMSAngularEntities1 db = new AMSAngularEntities1();
        public PurchasesController()
        {
            db.Configuration.ProxyCreationEnabled = false;

        }

        public List<PurchaseViewModel> GettblPurchases()
        {
            db.Configuration.ProxyCreationEnabled = true;
            List<tblPurchase> purchaseList = db.tblPurchases.ToList();
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
        // GET: api/Purchases
       
        // GET: api/Purchases/5
        [ResponseType(typeof(tblPurchase))]

        public List<VendorViewModel> GettblPurchase(int id)
        {
            db.Configuration.ProxyCreationEnabled = true;
            List<tblVendor> vendor = db.tblVendors.Where(x => x.vendorAssetType == id).ToList();

            List<VendorViewModel> venList = vendor.Select(x => new VendorViewModel
            {
                vendorId = Convert.ToInt32(x.vendorId),
                vendorName = x.vendorName,
                vendorType = x.vendorType,
                vendorAssetType = x.tblAssetType.assetTypeName,
                validFromStr = x.validFromStr,
                validToStr = x.validToStr,
                vendorAddress = x.vendorAddress
            }).ToList();
            return venList;
        }


        public PurchaseViewModel GetPurchase(int iid)
        {
            db.Configuration.ProxyCreationEnabled = true;
            tblPurchase porder = db.tblPurchases.Where(x => x.purchaseId == iid).FirstOrDefault();
            PurchaseViewModel pomodel = new PurchaseViewModel ();

            pomodel.purchaseId = porder.purchaseId;
            pomodel.purchaseOrderNo = porder.purchaseOrderNo;
            pomodel.assetName = porder.tblAssetDefinition.assetName;
            pomodel.assetType = porder.tblAssetType.assetTypeName;
            pomodel.purchaseAssetName = porder.purchaseAssetName;
            pomodel.purchaseAssetType = porder.purchaseAssetType;
            pomodel.purchaseVendorName = porder.purchaseVendorName;
            pomodel.quantity =  Convert.ToDecimal(porder.quantity);
            pomodel.vendorName = porder.tblVendor.vendorName;
            pomodel.purchaseDateStr = porder.purchaseDateStr;
            pomodel.purchaseDelivDateStr = porder.purchaseDelivDateStr;
            pomodel.purchaseStatus = porder.purchaseStatus;
            return pomodel;
        }

        public List<tblAssetType> GetTblAssetTypes(string name)
        {
            db.Configuration.ProxyCreationEnabled = true;
            tblAssetDefinition assetdef = db.tblAssetDefinitions.Where(x => x.assetName == name).FirstOrDefault();
            List<tblAssetType> typelist = db.tblAssetTypes.Where(x => x.assetTypeId == assetdef.assetType).ToList();
            return typelist;
        }


       
        // PUT: api/Purchases/5
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

        // POST: api/Purchases
        [ResponseType(typeof(tblPurchase))]
       
          public IHttpActionResult PosttblVendor(tblPurchase tblPurchase)
        {

            tblPurchase.purchaseDate = DateTime.Now;
                db.tblPurchases.Add(tblPurchase);
                db.SaveChanges();
                return CreatedAtRoute("DefaultApi", new { id = tblPurchase.purchaseId },tblPurchase);

        }


        // DELETE: api/Purchases/5
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