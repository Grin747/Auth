using Microsoft.EntityFrameworkCore;

namespace Auth.Models
{
    public sealed class ProjectContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        
        public ProjectContext(DbContextOptions<ProjectContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}