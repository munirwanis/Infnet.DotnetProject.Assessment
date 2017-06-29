using Infnet.DotnetProject.Assessment.Domain;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Infnet.DotnetProject.Assessment.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("DefaultConnection") {}

        public DbSet<Post> Posts { get; set; }

        public DbSet<Profile> Profiles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
