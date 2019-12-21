using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectDatabase.Classes
{
    // история местоположений
    public class HistoryOfLocation
    {
        public int Id { get; set; }
        public string NameOfPoint { get; set; }
        public int CountOfDepartures { get; set; }
        public int CountOfArrivals { get; set; }
    }
}
