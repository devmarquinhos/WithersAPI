using Microsoft.EntityFrameworkCore;
using WithersAPI.Models;

namespace WithersAPI.Data
{
    public class WithersContext : DbContext
    {
            public WithersContext(DbContextOptions<WithersContext> options) : base(options) {}
            public DbSet<Character> Characters { get; set; }
            public DbSet<User> Users { get; set; }

    }
}
