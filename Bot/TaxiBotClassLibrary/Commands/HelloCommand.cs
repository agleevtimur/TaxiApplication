using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TaxiBotClassLibrary.Commands
{
    public class HelloCommand : Command
    {
        public override string Name => "/hello";

        public override async void Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var text = message.Text;
            await client.SendTextMessageAsync(chatId, text + " " + message.Type);
        }
    }
}
