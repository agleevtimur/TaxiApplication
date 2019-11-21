using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectDatabase.Classes
{
    // история местоположений
    public class HistoryOfLocation
    {
        public string NameOfPoint { get; set; }
        public int CountOfDepartures { get; set; }
        public int CountOfArrivals { get; set; }
        public Location Location { get; set; }
    }
}
