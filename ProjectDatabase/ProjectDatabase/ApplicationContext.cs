using Microsoft.EntityFrameworkCore;
using ProjectDatabase.Classes;

namespace ProjectDatabase
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Request> Request { get; set; }
        public DbSet<HistoryOfRequest> HistoryOfRequest { get; set; }
        public DbSet<Answer> Answer { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<HistoryOfLocation> HistoryOfLocation { get; set; }

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }
        // создать пользователя Dmitriy в SQL Shell с паролем 29N05a96r
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Server=localhost;Database=projectdb;Port=5432;Username=Dmitriy;Password=29N05a96r");
        }
    }
}
