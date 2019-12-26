using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using TaxiBotClassLibrary.Commands;
using Telegram.Bot.Types;
using DataBase.Repository;
using TaxiBotClassLibrary.States;

namespace TaxiBotClassLibrary
{
    public static class Configurator
    {
        public static List<Command> GetCommands()
        {// глобальные комманды
            var list = new List<Command>();
            var baseType = typeof(Command);
            var heirs = Assembly.GetAssembly(baseType).GetTypes().Where(c => c.IsSubclassOf(baseType));
            foreach (var heir in heirs)
                list.Add((Command)Activator.CreateInstance(heir));
            return list;
        }

        // в этом словаре храняться пользователи и их контекст, содержащий конкретное состояние ссылку на след и метод обработки
        public static Dictionary<int, Context> Dictionary = new Dictionary<int, Context>();
    }
}
