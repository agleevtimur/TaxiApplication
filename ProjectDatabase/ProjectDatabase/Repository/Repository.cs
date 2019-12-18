using ProjectDatabase.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectDatabase.Repository
{
    public class Repository : IRepository
    {
        public void SaveUser(User user)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var isInUsers = false;
                foreach (User us in db.User)
                    if (us.Nickname == user.Nickname)
                    {
                        isInUsers = true;
                        us.CountOfTrip++;
                        break;
                    }
                if (isInUsers == false)
                    db.User.Add(user);
                db.SaveChanges();
            }
        }

        public void SaveRequest(Request request)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Request.Add(request);
                db.HistoryOfLocation.Find(request.DeparturePointId).CountOfDepartures++;
                db.HistoryOfLocation.Find(request.PlaceOfArrivalId).CountOfArrivals++;
                db.SaveChanges();
            }
        }

        public void DeleteRequest(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var request = db.Request.Find(id);
                db.Request.Remove(request);
                db.HistoryOfRequest.Add(
                    new HistoryOfRequest
                    {
                        Id = request.Id,
                        CountOfPeople = request.CountOfPeople,
                        DeparturePointId = request.DeparturePointId,
                        PlaceOfArrivalId = request.PlaceOfArrivalId,
                        DepartureTime = request.DepartureTime,
                        RequestTime = request.RequestTime,
                        User = request.User
                    });
                db.SaveChanges();
            }
        }

        public List<Location> GetLocations()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var list = new List<Location>();
                var locations = db.Location;
                foreach (Location location in locations)
                    list.Add(location);
                return list;
            }
        }

        public List<Request> GetRequests()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var list = new List<Request>();
                var requests = db.Request;
                foreach (Request request in requests)
                    list.Add(request);
                return list;
            }
        }
    }
}
