using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBase.Classes
{
    // запросы
    public class Request
    {
        public int Id { get; set; }
        public int DeparturePointId { get; set; }
        public int PlaceOfArrivalId { get; set; }
        public int CountOfPeople { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime RequestTime { get; set; }
        public User User { get; set; }
        public int UserId { get; set; } 
    }
}
