using Auth1.Core.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Auth1.Data
{
    public class AuthDbContext :IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        public AuthDbContext():base() { }
        public AuthDbContext(DbContextOptions<AuthDbContext> options):base(options)
        { 
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(IdentityContextSeed).Assembly).Seed();
         
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                 "server =.\\SQLEXPRESS2; Initial Catalog = AuthDb; TrustServerCertificate = True; Encrypt = False; Persist Security Info = True; User id = sa; password = 3012; ");
            }
        }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }       
    }
}
