using System;
using System.Collections.Generic;
using System.Text;

namespace DataBase.Classes
{
    // история запросов
    public class HistoryOfRequest
    {
        public int Id { get; set; }
        public int DeparturePointId { get; set; }
        public int PlaceOfArrivalId { get; set; }
        public int CountOfPeople { get; set; }
        public string DepartureTime { get; set; }
        public string RequestTime { get; set; }
        public User User { get; set; }
    }
}