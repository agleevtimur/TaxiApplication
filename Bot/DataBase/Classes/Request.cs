using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBase.Classes
{
    // запросы
    public class Request
    {
        public Request(int departurePointId, int placeOfArrivalId, int countOfPeople, DateTime departureTime, DateTime requestTime, Client client, int telegramId)
        {
            DeparturePointId = departurePointId;
            PlaceOfArrivalId = placeOfArrivalId;
            CountOfPeople = countOfPeople;
            DepartureTime = departureTime;
            RequestTime = requestTime;
            Client = client;
            TelegramId = telegramId;
        }

        public Request(int departurePointId, int placeOfArrivalId, int countOfPeople, DateTime departureTime, DateTime requestTime, int id, int userId)
        {
            Id = id;
            DeparturePointId = departurePointId;
            PlaceOfArrivalId = placeOfArrivalId;
            CountOfPeople = countOfPeople;
            DepartureTime = departureTime;
            RequestTime = requestTime;
            UserId = userId;
        }

        public int Id { get; set; }
        public int TelegramId { get; set; }
        public int DeparturePointId { get; set; }
        public int PlaceOfArrivalId { get; set; }
        public int CountOfPeople { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime RequestTime { get; set; }
        public Client Client { get; set; }
        public int UserId { get; set; }
    }
}
