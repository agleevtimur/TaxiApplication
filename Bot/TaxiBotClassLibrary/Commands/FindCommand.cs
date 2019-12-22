using System;
using System.Collections.Generic;
using System.Text;
using TaxiBotClassLibrary.InterCommands;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TaxiBotClassLibrary.Commands
{
    class FindCommand : Command
    {
        public override string Name => "/find";

        public override async void Execute(Message message, TelegramBotClient client)
        {
            var id = message.From.Id;
            //if (Configurator.Role.ContainsKey(id))
            //{
            //    Configurator.Role[id] = NDFAutomate<ICommand>.States[0];
            //}
            //else Configurator.Role.Add(id, NDFAutomate<ICommand>.States[0]);
            await client.SendTextMessageAsync(id, "Давайте найдем для вас попутчиков");
            StateMachine.Run();
            await client.SendTextMessageAsync(id, "Введите удобное вам время для заказа такси в формате HH:MM");
        }
    }
}
