using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectDatabase.Classes
{
    // ответ пользователей
    public class Answer
    {
        public int Id { get; set; }
        public string DepartureTime { get; set; }
        public string AnswerOfUser { get; set; }
        public string RequestTime { get; set; }
        public int CountOfPeople { get; set; }
        public Request Request { get; set; }
    }
}
