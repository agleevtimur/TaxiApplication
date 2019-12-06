using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TlgTaxiBotApp.Models
{
    public static class AppSettings
    {
        public static string Url { get; set; } = "https://telegrambotapp.azurewebsites.net:443/{0}";
        public static string Name { get; set; } = "FellowTaxiBot";

        public static string Key { get; set; } = "881019710:AAG6N_Zdm9RjCJbgeRT7g_1S2AXD6xDHN9o";
    }
}