using System;

namespace DataBase.Classes
{
    // история запросов
    public class HistoryOfRequest
    {
        public HistoryOfRequest(int departurePointId, int placeOfArrivalId, int countOfPeople, DateTime departureTime, DateTime requestTime, int clientId)
        {
            DeparturePointId = departurePointId;
            PlaceOfArrivalId = placeOfArrivalId;
            CountOfPeople = countOfPeople;
            DepartureTime = departureTime;
            RequestTime = requestTime;
            ClientId = clientId;
        }

        public int Id { get; set; }
        public int DeparturePointId { get; set; }
        public int PlaceOfArrivalId { get; set; }
        public int CountOfPeople { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime RequestTime { get; set; }
        public int ClientId { get; set; }
    }
}
