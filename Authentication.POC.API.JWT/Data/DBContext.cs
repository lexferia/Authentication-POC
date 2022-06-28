using Authentication.POC.API.JWT.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Authentication.POC.API.JWT.Data
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions options) 
            : base(options) { }

        public DbSet<User>? Users { get; set; }
        public DbSet<Role>? Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var roles = new Role[] {
                new Role { Id = Guid.NewGuid(), Name = "Administrator", CreatedAt = DateTime.UtcNow },
                new Role { Id = Guid.NewGuid(), Name = "Editor", CreatedAt = DateTime.UtcNow },
                new Role { Id = Guid.NewGuid(), Name = "Writer", CreatedAt = DateTime.UtcNow }
            };

            modelBuilder.Entity<Role>().HasData(roles);

            modelBuilder.Entity<User>().HasData(new User { 
                Id = Guid.NewGuid(),
                Name = "Lester Feria",
                Email = "dev@philippines.com",
                Enabled = true,
                CreatedAt = DateTime.UtcNow,
                Password = "123456",
                RoleId = roles.FirstOrDefault(item => item.Name.Equals("Administrator", StringComparison.InvariantCultureIgnoreCase))?.Id ?? Guid.Empty
            });
        }
    }
}
