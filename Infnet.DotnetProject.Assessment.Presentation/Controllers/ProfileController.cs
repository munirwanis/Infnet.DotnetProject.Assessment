using System.Net;
using System.Web.Mvc;
using Infnet.DotnetProject.Assessment.Domain;
using System.Collections.Generic;
using RestSharp;
using Infnet.DotnetProject.Assessment.Presentation.Helper;

namespace Infnet.DotnetProject.Assessment.Presentation.Controllers
{
    public class ProfileController : Controller
    {
        private const string URI = "api/profiles";

        // GET: Profile
        public ActionResult Index()
        {
            var profiles = RequestHelper.MakeRequest<List<Profile>>(URI, Method.GET);
            return View(profiles);
        }

        // GET: Profile/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var profile = RequestHelper.MakeRequest<Profile>($"{URI}/{id}", Method.GET);
            if (profile == null)
            {
                return HttpNotFound();
            }
            return View(profile);
        }

        // GET: Profile/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Profile/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,UserEmail,ProfilePicture,FirstName,LastName,NickName")] Profile profile)
        {
            profile.UserId = Session["UserId"].ToString();
            profile.UserEmail = Session["UserEmail"].ToString();
            profile.ProfilePicture = null;

            RequestHelper.MakeRequest<Profile>(URI, Method.POST, profile);
            return RedirectToAction("Index");
        }

        // GET: Profile/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var profile = RequestHelper.MakeRequest<Profile>($"{URI}/{id}", Method.GET);
            if (profile == null)
            {
                return HttpNotFound();
            }
            return View(profile);
        }

        // POST: Profile/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,UserEmail,ProfilePicture,FirstName,LastName,NickName")] Profile profile)
        {
            if (ModelState.IsValid)
            {
                RequestHelper.MakeRequest<Profile>($"{URI}/{profile.Id}", Method.PUT);
                return RedirectToAction("Index");
            }
            return View(profile);
        }

        // GET: Profile/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var profile = RequestHelper.MakeRequest<Profile>($"{URI}/{id}", Method.GET);
            if (profile == null)
            {
                return HttpNotFound();
            }
            return View(profile);
        }

        // POST: Profile/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RequestHelper.MakeRequest<Profile>($"{URI}/{id}", Method.DELETE);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
