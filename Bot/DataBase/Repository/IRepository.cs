using DataBase.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataBase.Repository
{
    public interface IRepository
    {
        void SaveUser(Client user);
        void SaveRequest(Request request);
        void DeleteRequest(int id);
        List<Location> GetLocations();
        List<Request> GetRequests();
    }
}
