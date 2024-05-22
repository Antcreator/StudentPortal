using Microsoft.EntityFrameworkCore;

namespace StudentPortal.Web.Data
{
    public class AppDbContext : DbContext
    {
        protected readonly IConfiguration _configuration;
        

        public AppDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseNpgsql(_configuration.GetConnectionString("ApiDatabase"));
        }

        public DbSet<Student> Students { get; set; }
    }
}
