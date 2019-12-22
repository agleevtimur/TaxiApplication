using DataBase.Classes;
using DataBase.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Taxi_Algorithm;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TaxiBotClassLibrary.InterCommands
{
    class SetStartPoint : ICommand
    {
        public async void Execute(Message message, TelegramBotClient client)
        {
            
            var id = message.From.Id;
            if (Check(message.Text))
            {
                await client.SendTextMessageAsync(id, "Куда едете?");
                
                var next = NDFAutomate<ICommand>.CurrentState.Transition[NDFAutomate<ICommand>.Functions[1]];
                NDFAutomate<ICommand>.CurrentState = next;

                Configurator.Values.Add(message.Text);
            }
            else await client.SendTextMessageAsync(id, "Данные неккоретны \nПопробуйте еще раз");
        }
        public bool Check(string message)
        {
            if (int.TryParse(message, out int location))
            {
                return location > 0 && location < Algorithm.GetLocations().Count - 1;
            }
            else return false;
        }
    }
}
