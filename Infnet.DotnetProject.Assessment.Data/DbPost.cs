using Infnet.DotnetProject.Assessment.Domain;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Infnet.DotnetProject.Assessment.Data
{
    public class DbPost : IData<Post>
    {
        public bool Delete(int id)
        {
            using (var db = new DatabaseContext())
            {
                var post = db.Posts.Find(id);
                db.Posts.Remove(post);
                return db.SaveChanges() > 0;
            }
        }

        public Post Get(int id)
        {
            using (var db = new DatabaseContext())
            {
                var post = db.Posts.Find(id);
                return post;
            }
        }

        public List<Post> GetAll()
        {
            using (var db = new DatabaseContext())
            {
                var posts = db.Posts.ToList();
                return posts;
            }
        }

        public bool Insert(Post entry)
        {
            using (var db = new DatabaseContext())
            {
                db.Posts.Add(entry);
                var success = db.SaveChanges() > 0;
                return success;
            }
        }

        public bool Update(int id, Post entry)
        {
            using (var db = new DatabaseContext())
            {
                db.Posts.Attach(entry);
                db.Entry(entry).State = EntityState.Modified;
                return db.SaveChanges() > 0;
            }
        }
    }
}
