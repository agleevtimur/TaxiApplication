using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectDatabase.Classes
{
    // запросы
    public class Request
    {
        public int Id { get; set; }
        public int DeparturePointId { get; set; }
        public int PlaceOfArrivalid { get; set; }
        public int CountOfPeople { get; set; }
        public string DepartureTime { get; set; }
        public string RequestTime { get; set; }
        public HistoryOfRequest HistoryOfRequest { get; set; }
        public Answer Answer { get; set; } 
        public Location Location { get; set; }
    }
}
