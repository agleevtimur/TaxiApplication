using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TaxiBotClassLibrary.Commands
{
    public class InfoCommand : Command
    {
        public override string Name => "/locations";

        public override async void Execute(Message message, TelegramBotClient client)
        {
            var id = message.From.Id;
            //var info = Taxi_Algorithm.Algorithm.GetLocations();
            //var locations = new StringBuilder();
            //foreach(var location in info)
            //{
            //    locations.AppendLine($"{location.Id} - {location.NameOfPoint}" + "\n");
            //}
            //await client.SendTextMessageAsync(id, locations.ToString());
            var info = Library.Init.info;
           if (info == null)
                info = "info bil raven null";
            await client.SendTextMessageAsync(id,info);
        }
    }
}
