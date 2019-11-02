using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectDatabase.Classes
{
    // запросы
    public class Requests
    {
        public int Id { get; set; }
        public string DeparturePoint { get; set; }
        public string PlaceOfArrival { get; set; }
        public int CountOfPeople { get; set; }
        public HistoryOfRequests HistoryOfRequests { get; set; }
        public int AnswerId { get; set; }
        public Answers Answers { get; set; } 
    }
}
