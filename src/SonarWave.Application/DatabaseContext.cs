using Microsoft.EntityFrameworkCore;
using SonarWave.Application.Entities;
using System.Reflection;

namespace SonarWave.Application
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> opt) : base(opt)
        {
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<Room> Rooms => Set<Room>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}