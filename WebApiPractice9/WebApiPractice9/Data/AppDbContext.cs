using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using WebApiPractice9.Models;

namespace WebApiPractice9.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
           
        }

        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .Property(b => b.Price)
                .HasColumnType("decimal(18,2)"); // sets SQL type explicitly
        }
    }
}
