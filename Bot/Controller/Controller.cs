using DataBase.Classes;
using DataBase.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
//using ProjectDatabase.Classes;
//using ProjectDatabase.Repository;
//TODO удалять заказы если текущая дата больше даты заказа
namespace Taxi_Algorithm
{
    public static class Extension
    {
        public static Request ParseToOrder(string time, string start, string finish, string countPerson, Client client)//возвращаем ордер из сообщения cmdFind
        {
            return new Request(int.Parse(start), int.Parse(finish), int.Parse(countPerson), AroundTime(time), DateTime.Now, client.Telegram, client.Nickname);
        }

        public static DateTime AroundTime(string time)
        {
            var today = DateTime.Now;
            var split = time.Split(':', '.', ',', ';');//возможный парсинг строки со временем
            var hours = int.Parse(split[0]);
            var minutes = int.Parse(split[1]);
            var aroundMinutes = (minutes / 5) * 5;
            var date = new DateTime(today.Year, today.Month, GetDay(hours, minutes), hours, aroundMinutes, 0);
            return date;
        }

        public static int GetDay(int hours, int minutes)
        {
            var totalM = minutes + 60 * hours;
            var today = DateTime.Now;
            var hoursCurrent = today.Hour;
            var minutesCurrent = today.Minute;
            var totalMCurrent = minutesCurrent + 60 * hoursCurrent;
            if (totalM < totalMCurrent)
            {//заказ на следующий день
                return today.Day + 1;
            }
            return today.Day;//заказ на этот день
        }

    }

    public class Complete
    {
        public readonly bool IsComplete;
        public readonly List<Request> enumReqs;
        public static Complete Empty = new Complete(false, null);
        public Complete(bool isComplete, List<Request> completeOrders)
        {
            IsComplete = isComplete;
            enumReqs = completeOrders;
        }

        public static Complete Orders3(IEnumerable<Request> suitedOrders, Request newestOrder)//ищем первую единицу
        {
            var order1first = suitedOrders.Where(x => x.CountOfPeople == 1).FirstOrDefault();
            if (order1first == null) return Empty;
            return new Complete(true, new List<Request> { newestOrder, order1first });
        }

        public static Complete Orders2(IEnumerable<Request> suitedOrders, Request newestOrder)
        {
            var completeOrders = new List<Request>();
            completeOrders.Add(newestOrder);
            var order2first = suitedOrders.Where(x => x.CountOfPeople == 2).FirstOrDefault();
            if (order2first != null)//сначала к двум пытаемся найти еще два
            {
                completeOrders.Add(order2first);
                return new Complete(true, completeOrders);
            }//иначе ищем две единицы
            var order1count = suitedOrders.Where(x => x.CountOfPeople == 1);
            if (order1count.Count() >= 2)
            {
                completeOrders.AddRange(order1count.Take(2));
                return new Complete(true, completeOrders);
            }
            return Empty;//если двух единиц нет
        }


        public static Complete Orders1(IEnumerable<Request> suitedOrders, Request newestOrder)
        {
            var completeOrders = new List<Request>();
            completeOrders.Add(newestOrder);
            var order3first = suitedOrders.Where(x => x.CountOfPeople == 3).FirstOrDefault();
            if (order3first != null)//ищем одну тройку
            {
                completeOrders.Add(order3first);
                return new Complete(true, completeOrders);
            }
            var order2first = suitedOrders.Where(x => x.CountOfPeople == 2).FirstOrDefault();
            if (order2first != null)//иначе ищем одну двойку и одну единицу
            {
                var order1first = suitedOrders.Where(x => x.CountOfPeople == 1).FirstOrDefault();
                if (order1first != null)
                {
                    completeOrders.Add(order2first);
                    completeOrders.Add(order1first);
                    return new Complete(true, completeOrders);
                }
            }//вопрос с елсе
            var order1count = suitedOrders.Where(x => x.CountOfPeople == 1);
            if (order1count.Count() >= 3)//ищем три и более единицы
            {
                completeOrders.AddRange(order1count.Take(3));
                return new Complete(true, completeOrders);
            }
            return Empty;
        }

        public static Complete TryComplete(int count, IEnumerable<Request> suitedOrders, Request newestOrder)
        {
            var completeOrders = new List<Request>();
            completeOrders.Add(newestOrder);
            switch (count)
            {
                case 1://ищем сначала 3, потом 2 1, потом 1 1 1
                    return Orders1(suitedOrders, newestOrder);
                case 2://ищем cначала 2, потом 1 1

                    return Orders2(suitedOrders, newestOrder);
                case 3://ищем сначала 1
                    return Orders3(suitedOrders, newestOrder);
                default:
                    return Empty;
            }
        }
    }

    public static class Algorithm
    {
        public static IRepository Repository = new Repository();

        public static IEnumerable<Request> Find(string time, string start, string finish, string countPerson, Client client)
        {

            Repository.SaveUser(client);
            var newestRequest = Extension.ParseToOrder(time, start, finish, countPerson, client);
            Repository.SaveRequest(newestRequest);//добавляем в репозиторий новый заказ
            var comlete = CheckCompleteOrder(newestRequest);//вызываем чекер на набор такси
            if (!comlete.IsComplete)
                return null;
            var reqs = comlete.enumReqs;
            foreach (var req in reqs)
            {
                Repository.DeleteRequest(req.Id);
            }
            return reqs;
        }

        public static List<Location> GetLocations()
        {
            return Repository.GetLocations();
        }

        public static Complete CheckCompleteOrder(Request newestRequest)//здесь должна проходить агрегация заказов, должны собираться и все
        {
            var suitedOrders = Repository.GetRequests().Where(x => SuitOrders(x, newestRequest));
            var count = newestRequest.CountOfPeople;
            var complete = Complete.TryComplete(count, suitedOrders, newestRequest);
            return complete;
        }

        public static bool SuitOrders(Request req1, Request req2)
        {
            return req1.DeparturePointId == req2.DeparturePointId
            && req1.PlaceOfArrivalId == req2.PlaceOfArrivalId
            && req1.DepartureTime == req2.DepartureTime
            && req1.Telegram != req2.Telegram;
        }

        public static string AroundTime(string time)
        {
            var today = DateTime.Today;
            var array = time.Split(':', '.', ',', ';');//возможный парсинг строки со временем
            var hours = int.Parse(array[0]);
            var minutes = int.Parse(array[1]);
            var aroundMinutes = (minutes / 5) * 5;
            var date = new DateTime(today.Year, today.Month, today.Day, hours, aroundMinutes, 0);
            return date.ToString();
        }

        public static int ParseCountPerson(string countPerson)
        {
            return int.Parse(countPerson);
        }


    }
}