using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Taxi_Algorithm;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TaxiBotClassLibrary.States
{
    class State2 : State
    {
        public override async void HandleForward(Message message, TelegramBotClient client)
        {// куда едем
            var id = message.From.Id;
            // не дадим пользователю выбрать Destination = Departure
            if (Check(message.Text) && Configurator.Dictionary[id].Values[1] != message.Text)
            {
                await client.SendTextMessageAsync(id, $"Сколько человек в вашей группе?");
                Configurator.Dictionary[id].TransitionTo(new FinishState());
                Configurator.Dictionary[id].Values[2] = message.Text;
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
        {
            Configurator.Dictionary[id].TransitionTo(new State1());
            await client.SendTextMessageAsync(id, "Откуда едете?");
        }
    }
}
