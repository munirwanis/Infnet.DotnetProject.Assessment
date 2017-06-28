using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Infnet.DotnetProject.Assessment.Domain;
using Infnet.DotnetProject.Assessment.Presentation.Models;

namespace Infnet.DotnetProject.Assessment.Presentation.Controllers
{
    public class PerfilsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Perfils
        public ActionResult Index()
        {
            return View(db.Profiles.ToList());
        }

        // GET: Perfils/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var perfil = db.Profiles.Find(id);
            if (perfil == null)
            {
                return HttpNotFound();
            }
            return View(perfil);
        }

        // GET: Perfils/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Perfils/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,UserEmail,ProfilePicture,FirstName,LastName,NickName")] Profile profile)
        {
            profile.UserId = Session["UserId"].ToString();
            profile.UserEmail = Session["UserEmail"].ToString();
            profile.ProfilePicture = null;

            db.Profiles.Add(profile);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Perfils/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var perfil = db.Profiles.Find(id);
            if (perfil == null)
            {
                return HttpNotFound();
            }
            return View(perfil);
        }

        // POST: Perfils/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,UserEmail,ProfilePicture,FirstName,LastName,NickName")] Profile profile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(profile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(profile);
        }

        // GET: Perfils/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var perfil = db.Profiles.Find(id);
            if (perfil == null)
            {
                return HttpNotFound();
            }
            return View(perfil);
        }

        // POST: Perfils/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var perfil = db.Profiles.Find(id);
            db.Profiles.Remove(perfil);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
