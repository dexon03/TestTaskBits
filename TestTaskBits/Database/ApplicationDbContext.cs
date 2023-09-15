using Microsoft.EntityFrameworkCore;
using TestTaskBits.Models;

namespace TestTaskBits.Database;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
    public DbSet<User> Users { get; set; }
}