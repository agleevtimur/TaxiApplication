using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TaxiBotClassLibrary.Commands
{
    public class InfoCommand : Command
    {
        public override string Name => "/info";

        public override async void Execute(Message message, TelegramBotClient client)
        {
            var id = message.From.Id;
            var info = @"список точек:
            1 - Деревня Универсиады
            2 - Главное здание
            3 - Уникс
            4 - Межлаука
            5 - Оренбургский тракт";
            await client.SendTextMessageAsync(id, info);
        }
    }
}
