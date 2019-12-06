using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using TaxiBotApp.Models.Commands;

namespace TaxiBotApp.Models
{
    public static class AppSettings
    {
        public static string BotName { get; } = "@fellow_taxi_bot";
        public static string Url { get; } = "https://taxibotapp.azurewebsites.net:443/{0}";
        public static string Key { get; } = "914484751:AAEVtwHtC7reRjL0xPcgAE2UgiD8jQVJR5k";

        public static List<Command> Commands { get; } = GetCommands();
        public static List<Command> GetCommands()
        {
            var list = new List<Command>();
            var baseType = typeof(Command);
            var heirs = Assembly.GetAssembly(baseType).GetTypes().Where(c => c.IsSubclassOf(baseType));
            foreach(var heir in heirs)
                list.Add((Command)Activator.CreateInstance(heir));
            return list;
        }
    }
}
