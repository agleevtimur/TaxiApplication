using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectDatabase.Classes
{
    // местоположения
    public class Location
    {
        public int NumberOfPoint { get; set; }
        public string NameOfPoint { get; set; }
        public Request Request { get; set; }
        public HistoryOfLocation HistoryOfLocation { get; set; }
    }
}
