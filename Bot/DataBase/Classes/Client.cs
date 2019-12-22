using System;
using System.Collections.Generic;
using System.Text;

namespace DataBase.Classes
{
    // пользователи
    public class Client
    {
        public Client(int telegram, string nickname)
        {
            Nickname = nickname;
            Telegram = telegram;
        }

        public int Id { get; set; }
        public string Nickname { get; set; }
        public int CountOfTrip { get; set; }

        public int Telegram { get; set; }
    }

    
}
