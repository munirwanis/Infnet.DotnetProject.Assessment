using Infnet.DotnetProject.Assessment.Domain;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Infnet.DotnetProject.Assessment.Data
{
    public class DbProfile : IData<Profile>
    {
        public bool Delete(int id)
        {
            using (var db = new DatabaseContext())
            {
                var profile = db.Profiles.Find(id);
                db.Profiles.Remove(profile);
                return db.SaveChanges() > 0;
            }
        }

        public Profile Get(int id)
        {
            using (var db = new DatabaseContext())
            {
                var profile = db.Profiles.Find(id);
                return profile;
            }
        }

        public List<Profile> GetAll()
        {
            using (var db = new DatabaseContext())
            {
                var profiles = db.Profiles.ToList();
                return profiles;
            }
        }

        public bool Insert(Profile entry)
        {
            using (var db = new DatabaseContext())
            {
                db.Profiles.Add(entry);
                var success = db.SaveChanges() > 0;
                return success;
            }
        }

        public bool Update(int id, Profile entry)
        {
            using (var db = new DatabaseContext())
            {
                db.Profiles.Attach(entry);
                db.Entry(entry).State = EntityState.Modified;
                return db.SaveChanges() > 0;
            }
        }
    }
}
