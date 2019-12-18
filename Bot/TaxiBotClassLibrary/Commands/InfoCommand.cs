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
            var info = "список точек:\n"+
            "1 - Деревня Универсиады\n" +
            "2 - Главное здание\n" +
            "3 - Уникс\n" +
            "4 - Межлаука\n" +
            "5 - Оренбургский тракт";
            await client.SendTextMessageAsync(id, info);
        }
    }
}
