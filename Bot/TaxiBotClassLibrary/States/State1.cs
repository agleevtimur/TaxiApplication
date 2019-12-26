using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Taxi_Algorithm;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TaxiBotClassLibrary.States
{
    class State1 : State
    {
        public override async void HandleForward(Message message, TelegramBotClient client)
        {// вводят откуда едут
            var id = message.From.Id;
            if (Check(message.Text))
            {
                await client.SendTextMessageAsync(id, $"Куда едете?");
                Configurator.Dictionary[id].TransitionTo(new State2());
                Configurator.Dictionary[id].Values[1] = message.Text;
            }
            else await client.SendTextMessageAsync(id, "Данные неккоретны \nПопробуйте еще раз");
        }
        public override bool Check(string message)
        {
            if (int.TryParse(message, out int location))
            {
                return location > 0 && location < Algorithm.GetLocations().Count - 1;
            }
            else return false;
        }

        public override async void HandleBack(int id, TelegramBotClient client)
        {// откат назад, если хотят поменять время
            Configurator.Dictionary[id].TransitionTo(new StartState());
            await client.SendTextMessageAsync(id, "Введите время еще раз");
        }
    }
}
