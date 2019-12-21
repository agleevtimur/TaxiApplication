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
        public DateTime DepartureTime { get; set; }
        public DateTime RequestTime { get; set; }
        public  Client User { get; set; }
        public int UserId { get; set; }
    }
}