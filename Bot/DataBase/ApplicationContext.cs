using Microsoft.EntityFrameworkCore;
using DataBase.Classes;

namespace DataBase
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Client> Client { get; set; }
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
            optionsBuilder.UseSqlServer("Server = tcp:taxibotapp20191219011147dbserver.database.windows.net, 1433; Initial Catalog = database; Persist Security Info = False; User ID = Timur; Password =29N05a96r; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30;");

        }


        public static void FillInClient(Client client)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Client.Add(client);
                db.SaveChanges();
            }
        }


        public static void FillInLocations(Location location)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Location.Add(location);
                db.HistoryOfLocation.Add(new HistoryOfLocation { NameOfPoint = location.NameOfPoint});
                db.SaveChanges();
            }
        }
    }
}
