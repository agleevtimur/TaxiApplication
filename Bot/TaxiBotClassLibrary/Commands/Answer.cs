using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TaxiBotClassLibrary.Commands
{
    class Answer : Command
    {
        public override string Name => "/ans";

        public override async void Execute(Message message, TelegramBotClient client)
        {
            var id = message.From.Id;
            var mes = message.Text;
            await client.SendTextMessageAsync(id, mes);
        }
    }
}
