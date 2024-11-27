
using FrameworkDigital_DesafioBackEnd.ORM.Entity.Lead;
using Microsoft.EntityFrameworkCore;

namespace FrameworkDigital_DesafioBackEnd.ORM.Context
{
    public class FrameworkDigitalDbContext : DbContext
    {
        public FrameworkDigitalDbContext(DbContextOptions<FrameworkDigitalDbContext> options) : base(options) { }

        public DbSet<LeadModel> Lead { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
