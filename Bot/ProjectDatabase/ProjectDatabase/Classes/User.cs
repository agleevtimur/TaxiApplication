using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectDatabase.Classes
{
    // пользователи
    public class User
    {
        public User(string nickname, long id)
        {
            Nickname = nickname;
            TelegramId = id;
        }

        public int Id { get; set; }
        public long TelegramId { get; set; }
        public string Nickname { get; set; }
        public int CountOfTrip { get; set; }
    }

    
}
