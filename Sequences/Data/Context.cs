using Microsoft.EntityFrameworkCore;
using Sequences.Data.Clients.Entities;

namespace Sequences.Data
{
    public class Context : DbContext
    {
        public DbSet<Client> Clients => Set<Client>();
    }
}