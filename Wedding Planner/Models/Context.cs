using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Wedding_Planner.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }

        public DbSet<Wedding> Weddings { get; set; }

        public DbSet<WeddingUserAssociation> WeddingUserAssociation { get; set; }
    }
}
