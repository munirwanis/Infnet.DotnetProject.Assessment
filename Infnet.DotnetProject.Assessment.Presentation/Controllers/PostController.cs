using System.Net;
using System.Web;
using System.Web.Mvc;
using Infnet.DotnetProject.Assessment.Domain;
using Infnet.DotnetProject.Assessment.Presentation.Models;
using System.Threading.Tasks;
using Infnet.DotnetProject.Assessment.Presentation.Blob;
using Infnet.DotnetProject.Assessment.Presentation.Helper;
using RestSharp;
using System.Collections.Generic;

namespace Infnet.DotnetProject.Assessment.Presentation.Controllers
{
    public class PostController : Controller
    {
        private const string URI = "api/posts";

        // GET: Post
        public ActionResult Index()
        {
            var posts = RequestHelper.MakeRequest<List<Post>>(URI, Method.GET);
            return View(posts);
        }

        // GET: Post/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var post = RequestHelper.MakeRequest<Post>($"{URI}/{id}", Method.GET);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: Post/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Post/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,UserEmail,Content,Image")] Post post)
        {
            post.UserEmail = Session["UserEmail"].ToString();
            post.UserId = Session["UserId"].ToString();
            post.Image = null;

            RequestHelper.MakeRequest<Post>(URI, Method.POST, post);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> PostImage(Post post, HttpPostedFileBase photo)
        {
            post.UserId = Session["UserId"].ToString();
            post.UserEmail = Session["UserEmail"].ToString();

            if (!(photo == null))
            {
                var imageService = new ImageService();
                var uploadImagem = await imageService.UploadImageAsync(photo);

                post.Image = uploadImagem.ToString();
            }

            RequestHelper.MakeRequest<Post>(URI, Method.POST, post);

            return RedirectToAction("Index");
        }

        // GET: Post/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var post = RequestHelper.MakeRequest<Post>($"{URI}/{id}", Method.GET);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Post/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,UserEmail,Content,Image")] Post post)
        {
            if (ModelState.IsValid)
            {
                RequestHelper.MakeRequest<Post>($"{URI}/{post.Id}", Method.PUT);
                return RedirectToAction("Index");
            }
            return View(post);
        }

        // GET: Post/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var post = RequestHelper.MakeRequest<Post>($"{URI}/{id}", Method.GET);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Post/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RequestHelper.MakeRequest<Post>($"{URI}/{id}", Method.DELETE);
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
