﻿using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectDatabase.Classes
{
    // запросы
    public class Request
    {
        public int Id { get; set; }
        public int DeparturePointId { get; set; }
        public int PlaceOfArrivalId { get; set; }
        public int CountOfPeople { get; set; }
        public string DepartureTime { get; set; }
        public string RequestTime { get; set; }
        public List<User> Users { get; set; }
    }
}
