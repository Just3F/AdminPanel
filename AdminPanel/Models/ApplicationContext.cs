using Microsoft.EntityFrameworkCore;

namespace AdminPanel.Models
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
        public DbSet<tblUser> tblUser { get; set; }
        public DbSet<tblUserVerification> tblUserVerification { get; set; }
    }
}
