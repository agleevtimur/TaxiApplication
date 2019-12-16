using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TaxiBotClassLibrary.InterCommands
{
    public class SetTime : InterCommand
    {
        public override async void Execute(Message message, TelegramBotClient client)
        {
            Configurator.index = 1;
            var id = message.From.Id;
            await client.SendTextMessageAsync(id, "Откуда едите?");
        }
    }
}
