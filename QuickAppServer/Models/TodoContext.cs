using Microsoft.EntityFrameworkCore;
using QuickAppServer.Models;

namespace QuickAppServer.Models
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
        }

        public DbSet<TodoItem> TodoItems { get; set; }

        public DbSet<User> User { get; set; }

        public DbSet<QuickAppServer.Models.BusinessPage> BusinessPage { get; set; }
    }
}