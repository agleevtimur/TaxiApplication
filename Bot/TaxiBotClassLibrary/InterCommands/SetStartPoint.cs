using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TaxiBotClassLibrary.InterCommands
{
    class SetStartPoint : InterCommand
    {
        public override async void Execute(Message message, TelegramBotClient client)
        {
            Configurator.index = 2;
            var id = message.From.Id;
            await client.SendTextMessageAsync(id, "Куда едете? //n Для справки //info");
        }
    }
}
