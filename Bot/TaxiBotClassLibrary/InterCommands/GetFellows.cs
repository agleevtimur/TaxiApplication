using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TaxiBotClassLibrary.InterCommands
{
    class GetFellows : ICommand
    {
        public bool Check(Message message)
        {
            throw new NotImplementedException();
        }

        public async void Execute(Message message, TelegramBotClient client)
        {
            var id = message.From.Id;
            List<string> data = new List<string>();
            foreach(var state in NDFAutomate<ICommand>.States)
            {
                data.Add(state.Container);
            }
            //метод ильдара
            await client.SendTextMessageAsync(id, "Запрос обрабатывается");
        }
    }
}
