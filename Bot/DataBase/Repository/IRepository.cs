using DataBase.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataBase.Repository
{
    public interface IRepository
    {
        Client GetUser(int id);
        int SaveUser(Client user);
        void SaveRequest(Request request);
        void DeleteRequest(int id);
        void DeleteCompletedRequest(int id);
        List<Location> GetLocations();
        List<Request> GetRequests();
        void SaveState(State state);
        State GetState(int telegramId);
    }
}
