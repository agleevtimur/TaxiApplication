using Microsoft.EntityFrameworkCore;
using ProjectDatabase.Classes;

namespace ProjectDatabase
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Users> Users { get; set; }
        public DbSet<Requests> Requests { get; set; }
        public DbSet<HistoryOfRequests> HistoryOfRequests { get; set; }
        public DbSet<Answers> Answers { get; set; }
        public DbSet<Locations> Locations { get; set; }
        public DbSet<HistoryOfLocations> HistoryOfLocations { get; set; }

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }
        // создать пользователя project в SQL Shell с паролем project
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Server=localhost;Database=projectdb;Port=5432;Username=project;Password=project");
        }
    }
}
