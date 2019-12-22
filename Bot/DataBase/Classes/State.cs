using System;
using System.Collections.Generic;
using System.Text;

namespace DataBase.Classes
{

    
    public class State
    {
        public int Id { get; set; }
        public int Telegram { get; set; }
        public int Status { get; set; }
        public State(int telegram,int status)
        {
            Telegram = telegram;
            Status = status;
        }
    }
}
