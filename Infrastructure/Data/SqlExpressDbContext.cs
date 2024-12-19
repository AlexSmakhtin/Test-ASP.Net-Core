using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using System.Data;

namespace Infrastructure.Data
{
    public class SqlExpressDbContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Founder> Founders { get; set; }

        public SqlExpressDbContext(DbContextOptions<SqlExpressDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<Client>()
                .Property(e => e.Type)
                .HasConversion(
                    v => v.ToString(),
                    v => (ClientTypes)Enum.Parse(typeof(ClientTypes), v));
        }
    }
}
