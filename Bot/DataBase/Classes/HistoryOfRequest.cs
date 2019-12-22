using System;
using System.Collections.Generic;
using System.Text;

namespace DataBase.Classes
{
    // история запросов
    public class HistoryOfRequest
    {
        public HistoryOfRequest(int departurePointId, int placeOfArrivalId, int countOfPeople, DateTime departureTime, DateTime requestTime, int telegram, string nickname)
        {
            DeparturePointId = departurePointId;
            PlaceOfArrivalId = placeOfArrivalId;
            CountOfPeople = countOfPeople;
            DepartureTime = departureTime;
            RequestTime = requestTime;
            Telegram = telegram;
            Nickname = nickname;
        }

        public int Id { get; set; }
        public int DeparturePointId { get; set; }
        public int PlaceOfArrivalId { get; set; }
        public int CountOfPeople { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime RequestTime { get; set; }
        public int Telegram { get; set; }
        public string Nickname { get; set; }
    }
}