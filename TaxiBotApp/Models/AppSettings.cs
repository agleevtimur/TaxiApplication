using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaxiBotApp.Models
{
    public static class AppSettings
    {
        public static string BotName { get; } = "@fellow_taxi_bot";
        public static string Url { get; } = "https://taxibotapp.azurewebsites.net:443/{0}";
        public static string Key { get; } = "914484751:AAEVtwHtC7reRjL0xPcgAE2UgiD8jQVJR5k";
    }
}
