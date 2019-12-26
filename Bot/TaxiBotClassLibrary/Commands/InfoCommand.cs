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
        {// список всех локаций
            var id = message.Chat.Id;
            var info = Taxi_Algorithm.Algorithm.GetLocations();
            var locations = new StringBuilder();
            foreach (var location in info)
            {
                locations.AppendLine($"{location.Id} - {location.NameOfPoint}" + "\n");
            }
            await client.SendTextMessageAsync(id, locations.ToString());
        }
    }
}
