using DataBase.Classes;
using System.Collections.Generic;
using System;

namespace DataBase
{
    public static class Initialize
    {
        public static void InitialLocation()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                ApplicationContext.FillInLocations(new Location { Id = 1, NameOfPoint = "Деревня Универсиады" });
                ApplicationContext.FillInLocations(new Location { Id = 2, NameOfPoint = "Кремлевская 35А" });
                ApplicationContext.FillInLocations(new Location { Id = 3, NameOfPoint = "КСК Уникс" });
                ApplicationContext.FillInLocations(new Location { Id = 4, NameOfPoint = "ТЦ Кольцо" });
                ApplicationContext.FillInLocations(new Location { Id = 5, NameOfPoint = "Кремлевская 16А" });
                ApplicationContext.FillInLocations(new Location { Id = 6, NameOfPoint = "Казанский Кремль" });
                ApplicationContext.FillInLocations(new Location { Id = 7, NameOfPoint = "Пушкина 32А" });
                ApplicationContext.FillInLocations(new Location { Id = 8, NameOfPoint = "Оренбургский тракт 10А" });
                ApplicationContext.FillInLocations(new Location { Id = 9, NameOfPoint = "Улица Баумана" });
                ApplicationContext.FillInLocations(new Location { Id = 10, NameOfPoint = "Центр семьи Казан" });
                ApplicationContext.FillInLocations(new Location { Id = 11, NameOfPoint = "Национальный музей Республики Татарстан" });
                ApplicationContext.FillInLocations(new Location { Id = 12, NameOfPoint = "Ак Барс Банк Арена" });
                ApplicationContext.FillInLocations(new Location { Id = 13, NameOfPoint = "Проспект Победы 91" });
                ApplicationContext.FillInLocations(new Location { Id = 14, NameOfPoint = "Кремлевская 18" });
                ApplicationContext.FillInLocations(new Location { Id = 15, NameOfPoint = "улица Межлаука" });
                ApplicationContext.FillInLocations(new Location { Id = 16, NameOfPoint = "улица Лево-Булочная" });
                ApplicationContext.FillInLocations(new Location { Id = 17, NameOfPoint = "Бустан" });
                ApplicationContext.FillInLocations(new Location { Id = 18, NameOfPoint = "Карла Маркса 10" });
                ApplicationContext.FillInLocations(new Location { Id = 19, NameOfPoint = "Карла Маркса 68" });
            }
        }
    }
}
