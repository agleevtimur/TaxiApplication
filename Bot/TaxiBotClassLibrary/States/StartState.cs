using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TaxiBotClassLibrary.States
{
    class StartState : State
    {
        public override async void HandleForward(Message message, TelegramBotClient client)
        {// вводят время
            var id = message.From.Id;
            if (Check(message.Text))
            {
                Configurator.Dictionary[id].TransitionTo(new State1());
                Configurator.Dictionary[id].Values[0] = message.Text;
                await client.SendTextMessageAsync(id, $"Откуда едете?");
            }
            else
                await client.SendTextMessageAsync(id, "Данные неккоретны \nПопробуйте еще раз");
            
        }
        public override bool Check(string message)
        {
            bool formatIsCorrect = TimeSpan.TryParse(message, out TimeSpan time);
            return formatIsCorrect;
        }

        public override async void HandleBack(int id, TelegramBotClient client)
        {
            Configurator.Dictionary[id].TransitionTo(new StartState());
            await client.SendTextMessageAsync(id, "Введите время еще раз");
        }
    }
}
