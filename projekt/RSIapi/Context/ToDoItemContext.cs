using Microsoft.EntityFrameworkCore;
using RSIapi.Models;

namespace RSIapi.Context
{
    public class ToDoItemContext : DbContext
    {
        public ToDoItemContext(DbContextOptions<ToDoItemContext> options)
            : base(options)
        {
        }
        public DbSet<ToDoItem> ToDoItems { get; set; }

        public DbSet<Board> Boards { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.ToDoItems)
                .WithOne(t => t.User)
                .HasForeignKey("UserId")
                .IsRequired(false);

            modelBuilder.Entity<Board>()
                 .HasMany(b => b.ToDoItems)
                 .WithOne(t => t.Board)
                 .HasForeignKey("BoardId")
                 .IsRequired();

        }   
    }
}
