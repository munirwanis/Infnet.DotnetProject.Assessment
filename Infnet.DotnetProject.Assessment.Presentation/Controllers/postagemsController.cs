using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Infnet.DotnetProject.Assessment.Domain;
using Infnet.DotnetProject.Assessment.Presentation.Models;
using System.Threading.Tasks;
using Infnet.DotnetProject.Assessment.Presentation.Blob;

namespace Infnet.DotnetProject.Assessment.Presentation.Controllers
{
    public class postagemsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        ImageService imageService = new ImageService();

        // GET: postagems
        public ActionResult Index()
        {
            return View(db.Posts.ToList());
        }

        // GET: postagems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post postagem = db.Posts.Find(id);
            if (postagem == null)
            {
                return HttpNotFound();
            }
            return View(postagem);
        }

        // GET: postagems/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: postagems/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,UserEmail,Content,Image")] Post postagem)
        {
            postagem.UserEmail = Session["UserEmail"].ToString();
            postagem.UserId = Session["UserId"].ToString();
            postagem.Image = null;

            db.Posts.Add(postagem);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> PostarImagem(Post model, HttpPostedFileBase photo)
        {
            model.UserId = Session["UserId"].ToString();
            model.UserEmail = Session["UserEmail"].ToString();

            if (!(photo == null))
            {
                ImageService imageService = new ImageService();
                var uploadImagem = await imageService.UploadImageAsync(photo);

                model.Image = uploadImagem.ToString();
            }

            db.Posts.Add(model);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: postagems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post postagem = db.Posts.Find(id);
            if (postagem == null)
            {
                return HttpNotFound();
            }
            return View(postagem);
        }

        // POST: postagems/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,UserEmail,Content,Image")] Post postagem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(postagem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(postagem);
        }

        // GET: postagems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post postagem = db.Posts.Find(id);
            if (postagem == null)
            {
                return HttpNotFound();
            }
            return View(postagem);
        }

        // POST: postagems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post postagem = db.Posts.Find(id);
            db.Posts.Remove(postagem);
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
