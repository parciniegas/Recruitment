using Microsoft.EntityFrameworkCore;
using Sequences.Data.Clients.Entities;
using Sequences.Data.Subjects.Entities;

namespace Sequences.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        { }

        public DbSet<Client> Clients => Set<Client>();
        public DbSet<Subject> Subjects => Set<Subject>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Subject>()
                .Property(b => b.LastUpdate)
                .HasDefaultValueSql("now()");
        }
    }
}