using FaceItStats.Api.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace FaceItStats.Api.Persistence
{
    public class FaceitDbContext(DbContextOptions<FaceitDbContext> options) : DbContext(options)
    {
        public DbSet<FaceitWebhookData> FaceitWebhookData { get; set; }

        public DbSet<MatchResult> MatchResult { get; set; }

        public DbSet<BetsSettings> BetsSettings { get; set; }

        public DbSet<ChallengeStats> ChallengeStats { get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(Const.ConnectionString);
        }
    }

}
