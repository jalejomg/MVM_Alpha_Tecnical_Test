using Alpha.Web.API.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Alpha.Web.API.Data
{
    public class AlphaDbContext : IdentityDbContext<AspNetUser>
    {
        public AlphaDbContext(DbContextOptions<AlphaDbContext> options) : base(options)
        {

        }

        public DbSet<Message> Messages { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AspNetUser>()
                .HasIndex(user => user.Id)
                .IsUnique();

            //modelBuilder.Entity<AspNetRole>()
            //    .HasIndex(user => user.Id)
            //    .IsUnique();

            modelBuilder.Entity<Message>()
                .HasIndex(message => message.Id)
                .IsUnique();

            modelBuilder.Entity<AuditLog>()
                .HasIndex(autitLog => autitLog.Id)
                .IsUnique();
        }
    }
}
