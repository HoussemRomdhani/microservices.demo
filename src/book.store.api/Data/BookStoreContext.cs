using book.store.api.Entities;
using Microsoft.EntityFrameworkCore;

namespace book.store.api.Data
{
    public class BookStoreContext : DbContext
    {
        public BookStoreContext(DbContextOptions<BookStoreContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>()
            .Property(p => p.Price)
           .HasColumnType("decimal(18,2)");
        }

        public DbSet<Book> Books { get; set; }

    }
}
