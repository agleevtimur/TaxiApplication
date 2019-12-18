using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectDatabase.Classes
{
    // пользователи
    public class User
    {
        public int Id { get; set; }
        public string Nickname { get; set; }
        public int CountOfTrip { get; set; }
    }
}
