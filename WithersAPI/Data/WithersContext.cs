using Microsoft.EntityFrameworkCore;
using WithersAPI.Models;

namespace WithersAPI.Data
{
    public class WithersContext : DbContext
    {
            public WithersContext(DbContextOptions<WithersContext> options) : base(options) {}
            public DbSet<Character> Characters { get; set; }
            public DbSet<User> Users { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<User>()
                    .HasMany(u => u.Characters)
                    .WithOne(c => c.User)
                    .HasForeignKey(c => c.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            }

    }
}
