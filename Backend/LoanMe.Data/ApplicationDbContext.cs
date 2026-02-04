using LoanMe.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace LoanMe.Data {
    public class ApplicationDbContext : DbContext {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<DraftLoan> DraftLoans { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ActiveLoan> ActiveLoans { get; set; }
        public DbSet<BlacklistMobile> BlacklistMobiles { get; set; }
        public DbSet<BlacklistEmailDomain> BlackListEmailDomains { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
