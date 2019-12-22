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
        public static Request ParseToOrder(string time, string start, string finish, string countPerson, int clientId)//возвращаем ордер из сообщения cmdFind
        {
            return new Request(int.Parse(start), int.Parse(finish), int.Parse(countPerson), AroundTime(time), DateTime.Now, clientId);
        }

        private static DateTime AroundTime(string time)
        {
            var today = DateTime.Now;
            var split = time.Split(':', '.', ',', ';');//возможный парсинг строки со временем
            var hours = int.Parse(split[0]);
            var minutes = int.Parse(split[1]);
            var aroundMinutes = (minutes / 10) * 10;
            var date = new DateTime(today.Year, today.Month, GetDay(hours, minutes), hours, aroundMinutes, 0);
            return date;
        }

        private static int GetDay(int hours, int minutes)
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

    class Complete
    {
        public readonly bool IsComplete;
        public readonly List<Request> enumReqs;
        public static Complete Empty = new Complete(false, null);
        public Complete(bool isComplete, List<Request> completeOrders)
        {
            IsComplete = isComplete;
            enumReqs = completeOrders;
        }

        static Complete Orders3(IEnumerable<Request> suitedOrders, Request newestOrder)//ищем первую единицу
        {
            var order1first = GetFirstRequest(suitedOrders, 1);
            if (order1first == null) return Empty;//ищем одну единицу
            return new Complete(true, new List<Request> { newestOrder, order1first });
        }

        static Complete Orders2(IEnumerable<Request> suitedOrders, Request newestOrder)
        {
            var completeOrders = new List<Request> { newestOrder };
            var order2first = GetFirstRequest(suitedOrders, 2);
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

        private static Request GetFirstRequest(IEnumerable<Request> requests, int countOfPeople)
        {
            return requests.FirstOrDefault(x => x.CountOfPeople == countOfPeople);
        }

        static Complete Orders1(IEnumerable<Request> suitedOrders, Request newestOrder)
        {
            var completeOrders = new List<Request> { newestOrder };
            var order3first = GetFirstRequest(suitedOrders, 3);
            if (order3first != null)//ищем одну тройку
            {
                completeOrders.Add(order3first);
                return new Complete(true, completeOrders);
            }
            var req2First = GetFirstRequest(suitedOrders, 2);
            if (req2First != null)//иначе ищем одну двойку и одну единицу
            {
                var req1First = GetFirstRequest(suitedOrders, 1);
                if (req1First != null)
                {
                    completeOrders.Add(req2First);
                    completeOrders.Add(req1First);
                    return new Complete(true, completeOrders);
                }
            }
            var req1Many = suitedOrders.Where(x => x.CountOfPeople == 1);
            if (req1Many.Count() >= 3)//ищем три и более единицы
            {
                completeOrders.AddRange(req1Many.Take(3));
                return new Complete(true, completeOrders);
            }
            return Empty;
        }

        public static Complete TryComplete(int count, IEnumerable<Request> suitedOrders, Request newestOrder)
        {
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
        private static IRepository Repository = new Repository();

        public static IEnumerable<Client> Find(string time, string start, string finish, string countPerson, Client client)
        {
            DeleteOldRequests();
            var idClient = Repository.SaveUser(client);//сохраняем нового пользователя, либо обновляем его данные
            var newRequest = Extension.ParseToOrder(time, start, finish, countPerson, idClient);//собираем из данных реквест
            Repository.SaveRequest(newRequest);//добавляем в репозиторий новый заказ,получаем Id для заказа
            var comlete = AggregateComplete(newRequest);//вызываем чекер на набор такси, возвращает класс Complete
            if (!comlete.IsComplete)//если такси не собрано, возвращаем null
                return null;
            var reqs = comlete.enumReqs;//иначе возвращаем список собранных заказов
            reqs.ForEach(x => Repository.DeleteCompletedRequest(x.Id));//и удаляем данные реквесты из таблицы, обновляем значения таблиц HistoryOfLocation(логика репозитория)
            return reqs.Select(x => Repository.GetUser(x.ClientId));
        }

        public static State GetState(int id)
        {
            return Repository.GetState(id);
        }

        public static void SaveState(State state)
        {
            Repository.SaveState(state);
        }
        public static List<Location> GetLocations()//для команды /locations
        {
            return Repository.GetLocations();
        }

        private static void DeleteOldRequests()
        {
            var allReqs = Repository.GetRequests();
            allReqs.ForEach(x =>
            {
                if (x.DepartureTime < DateTime.Now)
                {
                    Repository.DeleteRequest(x.Id);
                }
            });
        }

        private static Complete AggregateComplete(Request newestRequest)//здесь проходит агрегация заказов, попытка собрать группу попутчиков
        {
            var suitedOrders = Repository.GetRequests().Where(x => SuitOrders(x, newestRequest));
            var count = newestRequest.CountOfPeople;
            var complete = Complete.TryComplete(count, suitedOrders, newestRequest);
            return complete;
        }

        private static bool SuitOrders(Request req1, Request req2)//предикат для поиска похожих заказов
        {
            return req1.DeparturePointId == req2.DeparturePointId
            && req1.PlaceOfArrivalId == req2.PlaceOfArrivalId
            && req1.DepartureTime == req2.DepartureTime
            && req1.ClientId != req2.ClientId;
        }
    }
}