using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TaxiBotClassLibrary.InterCommands
{
    class SetDestination : InterCommand
    {
        public override async void Execute(Message message, TelegramBotClient client)
        {
            Configurator.index = 3;
            var id = message.From.Id;
            await client.SendTextMessageAsync(id, "Сколько человек в вашей группе?");
        }
    }
}
