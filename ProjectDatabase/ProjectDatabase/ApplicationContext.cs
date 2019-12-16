using Microsoft.EntityFrameworkCore;
using ProjectDatabase.Classes;

namespace ProjectDatabase
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Request> Request { get; set; }
        public DbSet<HistoryOfRequest> HistoryOfRequest { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<HistoryOfLocation> HistoryOfLocation { get; set; }
        public ApplicationContext()
        {
            Database.EnsureCreated();
        }
        // создать пользователя project в SQL Shell с паролем project
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Server=localhost;Database=postgres;Port=5432;Username=project;Password=project");
        }

        public static void FillInLocations(Location location)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Location.Add(location);
                db.HistoryOfLocation.Add(new HistoryOfLocation { Id = location.Id, NameOfPoint = location.NameOfPoint});
                db.SaveChanges();
            }
        }
    }
}
