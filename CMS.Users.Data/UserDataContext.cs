using CMS.Users.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace CMS.Users.Data
{
    [ExcludeFromCodeCoverage]
    public class UserDataContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public UserDataContext(DbContextOptions<UserDataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
#if SHOW_FLUENT_API
            // Should you want to see an example of Fluent API
            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<User>().Property(u => u.Username).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<User>().Property(a => a.CreatedOn).ValueGeneratedOnAdd();
            modelBuilder.Entity<User>().Property(a => a.LastUpdatedOn).IsConcurrencyToken().ValueGeneratedOnAddOrUpdate();
#else
            // Method intentionally left empty.
#endif
        }


    }
}
