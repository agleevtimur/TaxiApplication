using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using TaxiBotClassLibrary.InterCommands;
using TaxiBotClassLibrary.Commands;
using TaxiBotClassLibrary;

namespace TaxiBotApp.Models
{
    public static class AppSettings
    {
        public static string BotName { get; } = "@fellow_taxi_bot";
        public static string Url { get; } = "https://taxibotapp20191222010851.azurewebsites.net:443/{0}";
        public static string Key { get; } = "1021106672:AAGb30pzNGinQ2UUS0TpRC0pzxCH9bq__OI";
        //public static List<Command> Commands { get; } = Configurator.GetCommands();
        //public static List<InterCommand> InternalCommands { get; } = Configurator.GetInternalCommand();
       
    }
}
