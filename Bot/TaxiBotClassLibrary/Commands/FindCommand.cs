using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaxiBotClassLibrary.InterCommands;
using Telegram.Bot;
using Telegram.Bot.Types;
using DataBase.Repository;
using DataBase.Classes;

namespace TaxiBotClassLibrary.Commands
{
    class FindCommand : Command
    {
        public override string Name => "/find";
        public override async void Execute(Message message, TelegramBotClient client)
        {
            var id = message.From.Id;
            await client.SendTextMessageAsync(id, "Давайте найдем для вас попутчиков");
            StateMachine.Run();
            
            await client.SendTextMessageAsync(id, "Введите удобное вам время для заказа такси в формате HH:MM");
        }
    }
}
