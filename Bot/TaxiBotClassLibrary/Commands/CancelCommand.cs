using System;
using System.Collections.Generic;
using System.Text;
using TaxiBotClassLibrary.InterCommands;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TaxiBotClassLibrary.Commands
{
    class CancelCommand : Command
    {
        public override string Name => "/cancel";

        public override async void Execute(Message message, TelegramBotClient client)
        {
            var id = message.From.Id;
            StateMachine.Revoke();
            await client.SendTextMessageAsync(id, "Вы прекратили поиск");
        }
    }
}
