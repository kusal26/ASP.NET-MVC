using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using webapp2.Models;

namespace webapp2.Data
{
    public class webapp2Context : DbContext
    {
        public webapp2Context (DbContextOptions<webapp2Context> options)
            : base(options)
        {
        }

        public DbSet<webapp2.Models.Player> Player { get; set; } = default!;

        public DbSet<webapp2.Models.Country> Country { get; set; } = default!;

        public DbSet<webapp2.Models.Club> Club { get; set; } = default!;
    }
}
