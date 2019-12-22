using DataBase.Classes;
using DataBase.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TaxiBotClassLibrary.InterCommands
{
    public class SetTime : ICommand
    {
        public async void Execute(Message message, TelegramBotClient client)
        { 
            var id = message.From.Id;
            if (Check(message.Text))
            {
                await client.SendTextMessageAsync(id, "Откуда едете? ");

                var next = NDFAutomate<ICommand>.CurrentState.Transition[NDFAutomate<ICommand>.Functions[0]];
                NDFAutomate<ICommand>.CurrentState = next;
                Configurator.Values.Add(message.Text);
            }
            else
            {
                await client.SendTextMessageAsync(id, "Данные неккоретны \nПопробуйте еще раз");
            }

        }

        public bool Check(string message)
        {
            bool formatIsCorrect = TimeSpan.TryParse(message, out TimeSpan time);
            return formatIsCorrect;
        }
    }
}
