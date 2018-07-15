using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.Models
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
        public DbSet<tblUser> tblUser { get; set; }
        public DbSet<tblUserVerification> tblUserVerification { get; set; }
        public DbSet<vlGeneralSettings> vlGeneralSettings { get; set; }
        public DbSet<tblPost> tblPost { get; set; }
        public DbSet<tblCategory> tblCategory { get; set; }
        public DbSet<tblMainCategory> tblMainCategory { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<xPostCategory>()
            //    .HasKey(t => new { t.PostId, t.CategoryId });
            base.OnModelCreating(modelBuilder);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

        }
    }
}
