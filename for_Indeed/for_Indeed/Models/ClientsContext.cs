using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace for_Indeed.Models
{
    public class ClientsContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public ClientsContext(DbContextOptions<ClientsContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
