using DataBase.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataBase.Repository
{
    public class Repository : IRepository
    {
        private static ApplicationContext db = new ApplicationContext();
        public void SaveUser(Client client)
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
                {
                    client.CountOfTrip++;
                    db.Client.Add(client);
                }
                db.SaveChanges();
        }

        public void SaveRequest(Request request)
        {
                db.Request.Add(request);
                db.HistoryOfLocation.Find(request.DeparturePointId).CountOfDepartures++;
                db.HistoryOfLocation.Find(request.PlaceOfArrivalId).CountOfArrivals++;
                db.SaveChanges();
        }

        public void DeleteRequest(int id)
        {
                var req = db.Request.Find(id);
                db.Request.Remove(req);
                db.HistoryOfRequest.Add(
                    new HistoryOfRequest(req.DeparturePointId, req.PlaceOfArrivalId, req.CountOfPeople, req.DepartureTime, req.RequestTime, req.Telegram, req.Nickname)
                    ); 
                db.SaveChanges();
        }

        public List<Location> GetLocations()
        {
                var list = new List<Location>();
                var locations = db.Location;
                foreach (var location in locations)
                    list.Add(location);
                return list;
        }

        public List<Request> GetRequests()
        {
                var list = new List<Request>();
                var requests = db.Request;
                foreach (Request request in requests)
                    list.Add(request);
                return list;
        }
    }
}
