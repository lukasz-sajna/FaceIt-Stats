using FaceItStats.Api.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace FaceItStats.Api.Persistence
{
    public class FaceitDbContext : DbContext
    {
        public DbSet<FaceitWebhookData> FaceitWebhookData { get; set; }

        public FaceitDbContext(DbContextOptions<FaceitDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(Const.ConnectionString);
        }
    }

}
