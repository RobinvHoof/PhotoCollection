using BlazorApp.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.Infrastructure.Repositories
{
    public class PhotoContext : DbContext
    {
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public PhotoContext(DbContextOptions<PhotoContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Comment entity configuration
            modelBuilder.Entity<Comment>().Property(c => c.Subject).HasMaxLength(100);
            modelBuilder.Entity<Comment>().Property(c => c.Body).HasMaxLength(300);
        }
    }
}
