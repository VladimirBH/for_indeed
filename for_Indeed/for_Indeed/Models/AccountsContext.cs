using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace for_Indeed.Models
{
    public class AccountsContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public AccountsContext(DbContextOptions<AccountsContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
