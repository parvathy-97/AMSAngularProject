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
    public class LoginsController : ApiController
    {
        private AMSAngularEntities1 db = new AMSAngularEntities1();

        public tblLogin GetTblLogins(string username, string password)
        {
            return db.tblLogins.Where(x => x.username == username && x.password == password).FirstOrDefault();
        }
        
                
        // GET: api/Logins
        public IQueryable<tblLogin> GettblLogins()
        {
            return db.tblLogins;
        }

        // GET: api/Logins/5
        [ResponseType(typeof(tblLogin))]
        public IHttpActionResult GettblLogin(int id)
        {
            tblLogin tblLogin = db.tblLogins.Find(id);
            if (tblLogin == null)
            {
                return NotFound();
            }

            return Ok(tblLogin);
        }

        // PUT: api/Logins/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PuttblLogin(int id, tblLogin tblLogin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblLogin.loginId)
            {
                return BadRequest();
            }

            db.Entry(tblLogin).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblLoginExists(id))
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

        // POST: api/Logins
        [ResponseType(typeof(tblLogin))]
        public IHttpActionResult PosttblLogin(tblLogin tblLogin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tblLogins.Add(tblLogin);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tblLogin.loginId }, tblLogin);
        }

        // DELETE: api/Logins/5
        [ResponseType(typeof(tblLogin))]
        public IHttpActionResult DeletetblLogin(int id)
        {
            tblLogin tblLogin = db.tblLogins.Find(id);
            if (tblLogin == null)
            {
                return NotFound();
            }

            db.tblLogins.Remove(tblLogin);
            db.SaveChanges();

            return Ok(tblLogin);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblLoginExists(int id)
        {
            return db.tblLogins.Count(e => e.loginId == id) > 0;
        }
    }
}