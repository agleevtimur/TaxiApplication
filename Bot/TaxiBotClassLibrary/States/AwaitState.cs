using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TaxiBotClassLibrary.States
{// состояние ожидания
    class AwaitState : State
    {
        public override bool Check(string message)
        {
            throw new NotImplementedException();
        }

        public override async void HandleBack(int id, TelegramBotClient client)
        {
            await client.SendTextMessageAsync(id, "Ждите...");
        }

        public override void HandleForward(Message message, TelegramBotClient client)
        {
            HandleBack(message.From.Id, client);
        }
    }
}
