using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBase.Classes
{
    // местоположения
    public class Location
    {
        public int Id { get; set; }
        public string NameOfPoint { get; set; }
    }
}
