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
                ApplicationContext.FillInLocations(new Location { NameOfPoint = "Деревня Универсиады" });
                ApplicationContext.FillInLocations(new Location { NameOfPoint = "Кремлевская 35А" });
                ApplicationContext.FillInLocations(new Location { NameOfPoint = "КСК Уникс" });
                ApplicationContext.FillInLocations(new Location { NameOfPoint = "ТЦ Кольцо" });
                ApplicationContext.FillInLocations(new Location { NameOfPoint = "Кремлевская 16А" });
                ApplicationContext.FillInLocations(new Location { NameOfPoint = "Казанский Кремль" });
                ApplicationContext.FillInLocations(new Location { NameOfPoint = "Пушкина 32А" });
                ApplicationContext.FillInLocations(new Location { NameOfPoint = "Оренбургский тракт 10А" });
                ApplicationContext.FillInLocations(new Location { NameOfPoint = "Улица Баумана" });
                ApplicationContext.FillInLocations(new Location { NameOfPoint = "Центр семьи Казан" });
                ApplicationContext.FillInLocations(new Location { NameOfPoint = "Национальный музей Республики Татарстан" });
                ApplicationContext.FillInLocations(new Location { NameOfPoint = "Ак Барс Банк Арена" });
                ApplicationContext.FillInLocations(new Location { NameOfPoint = "Проспект Победы 91" });
                ApplicationContext.FillInLocations(new Location { NameOfPoint = "Кремлевская 18" });
                ApplicationContext.FillInLocations(new Location { NameOfPoint = "улица Межлаука" });
                ApplicationContext.FillInLocations(new Location { NameOfPoint = "улица Лево-Булочная" });
                ApplicationContext.FillInLocations(new Location { NameOfPoint = "Бустан" });
                ApplicationContext.FillInLocations(new Location { NameOfPoint = "Карла Маркса 10" });
                ApplicationContext.FillInLocations(new Location { NameOfPoint = "Карла Маркса 68" });
            }
        }
    }
}
