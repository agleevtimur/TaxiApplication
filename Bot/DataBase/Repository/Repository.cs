using DataBase.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataBase.Repository
{
    public class Repository : IRepository
    {
        public void SaveUser(Client client)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var isInUsers = false;
                foreach (Client cl in db.Client)
                    if (cl.Nickname == client.Nickname)
                    {
                        isInUsers = true;
                        cl.CountOfTrip++;
                        break;
                    }
                if (isInUsers == false)
                    db.Client.Add(client);
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
                        CountOfPeople = request.CountOfPeople,
                        DeparturePointId = request.DeparturePointId,
                        PlaceOfArrivalId = request.PlaceOfArrivalId,
                        DepartureTime = request.DepartureTime,
                        RequestTime = request.RequestTime,
                        UserId = request.UserId
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
                foreach (var location in locations)
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
