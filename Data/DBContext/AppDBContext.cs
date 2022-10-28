using Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data.DBContext
{
    public class AppDBContext : IdentityDbContext
    {
        private readonly DbContextOptions _options;

        public AppDBContext(DbContextOptions options) : base(options)
        {
            _options = options;
        }

        public DbSet<Account> AuthDetails { get; set; }
        public DbSet<AuthToken> AuthTokens { get; set; }
        public DbSet<SyncingInformation> SyncingInformation { get; set; }
        public DbSet<SyncingJob> SyncingJob { get; set; }
        public DbSet<SyncingJobEntity> SyncingJobEntity { get; set; }
        public DbSet<SyncingLog> SyncingLog { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
