using DataBase.Classes;
using System;
using System.Collections.Generic;

namespace DataBase.Repository
{
    public class Repository : IRepository
    {
        private static ApplicationContext db = new ApplicationContext();
        public void SaveState(State state)
        {
            var isInStates = false;
            foreach (State sl in db.State)
            {
                if (sl.Telegram == state.Telegram)
                {
                    isInStates = true;
                    sl.Status = state.Status;//reload state
                    break;
                }
            }
            if (isInStates == false)
            {
                db.State.Add(state);
            }
            db.SaveChanges();
        }

        public State GetState(int telegram)
        {
            foreach(State s1 in db.State)
            {
                if(s1.Telegram==telegram)
                {
                    return s1;
                }
            }

            throw new ArgumentException();
        }
        public int SaveUser(Client client)
        {
            var id = 0;
            var isInUsers = false;
            foreach (Client cl in db.Client)
                if (cl.Nickname == client.Nickname)
                {
                    isInUsers = true;
                    cl.CountOfTrip++;
                    id= cl.Id;
                    break;
                }
            if (isInUsers == false)
            {
                client.CountOfTrip++;
                db.Client.Add(client);
                id =client.Id;
            }
            db.SaveChanges();
            return id;
        }

        public void SaveRequest(Request request)
        {
            db.Request.Add(request);
            db.HistoryOfLocation.Find(request.DeparturePointId).CountOfDepartures++;
            db.HistoryOfLocation.Find(request.PlaceOfArrivalId).CountOfArrivals++;
            db.SaveChanges();
        }

        public void DeleteCompletedRequest(int id)
        {
            var req = db.Request.Find(id);
            db.Request.Remove(req);
            db.HistoryOfRequest.Add(
            new HistoryOfRequest(req.DeparturePointId, req.PlaceOfArrivalId, req.CountOfPeople, req.DepartureTime, req.RequestTime, req.ClientId)
            );
            db.SaveChanges();
        }

        public void DeleteRequest(int id)
        {
            var req = db.Request.Find(id);
            db.Request.Remove(req);
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

        public Client GetUser(int id)
        {
            return db.Client.Find(id);
        }
    }
}