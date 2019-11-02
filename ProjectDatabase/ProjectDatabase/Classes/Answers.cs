using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectDatabase.Classes
{
    // ответы пользователей
    public class Answers
    {
        public int Id { get; set; }
        public string DepartureTime { get; set; }
        public string AnswerOfUser { get; set; }
        public int RequestId { get; set; }
        public Requests Requests { get; set; }
    }
}
