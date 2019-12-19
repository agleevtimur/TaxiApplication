using ProjectDatabase.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectDatabase.Repository
{
    public interface IRepository
    {
        void SaveUser(User user);
        void SaveRequest(Request request);
        void DeleteRequest(int id);
        List<Location> GetLocations();
        List<Request> GetRequests();
    }
}
