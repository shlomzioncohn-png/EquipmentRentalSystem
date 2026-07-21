using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class Context : DbContext
    {
        public DbSet<User> Users { get; set; } = default!;
        public DbSet<Business> Businesses { get; set; } = default!;
        public DbSet<Item> Item { get; set; } = default!;

        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        
    }
}






   