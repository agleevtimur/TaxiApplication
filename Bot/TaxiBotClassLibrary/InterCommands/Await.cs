using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TaxiBotClassLibrary.InterCommands
{
    class Await : ICommand
    {
        public bool Check(string message)
        {
            throw new NotImplementedException();
        }

        public async void Execute(Message message, TelegramBotClient client)
        {
            var id = message.From.Id;
            await client.SendTextMessageAsync(id, "Поиск идет...");

        }
    }
}
