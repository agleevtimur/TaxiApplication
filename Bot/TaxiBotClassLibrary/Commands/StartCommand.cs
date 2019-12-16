using System;
using System.Collections.Generic;
using System.Text;
using TaxiBotClassLibrary.InterCommands;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TaxiBotClassLibrary.Commands
{
    class StartCommand : Command
    {
        public override string Name => "/start";

        public override async void Execute(Message message, TelegramBotClient client)
        {
            var id = message.From.Id;
            await client.SendTextMessageAsync(id, "Давайте найдем для вас попутчиков");
            StateMachine.Run();
            Configurator.
            await client.SendTextMessageAsync(id, "Введите удобное вам время для заказа такси");
        }
    }
}
