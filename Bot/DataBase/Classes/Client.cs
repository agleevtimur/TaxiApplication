using System;
using System.Collections.Generic;
using System.Text;

namespace DataBase.Classes
{
    // пользователи
    public class Client
    {
        public Client(string nickname)
        {
            Nickname = nickname;
        }

        public int Id { get; set; }
        public string Nickname { get; set; }
        public int CountOfTrip { get; set; }
    }

    
}
