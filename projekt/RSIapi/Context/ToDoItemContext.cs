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
    }
}
