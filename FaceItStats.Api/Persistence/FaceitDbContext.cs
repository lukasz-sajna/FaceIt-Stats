using FaceItStats.Api.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FaceItStats.Api.Persistence
{
    public class FaceitDbContext : DbContext
    {
        public DbSet<FaceitWebhookData> FaceitWebhookData { get; set; }

        public DbSet<MatchResult> MatchResult { get; set; }

        public DbSet<BetsSettings> BetsSettings { get; set; }

        public DbSet<ChallangeStats> ChallangeStats { get; set;}

        public FaceitDbContext(DbContextOptions<FaceitDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(Const.ConnectionString);
        }

        internal Task FirstOrDefaultAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

}
