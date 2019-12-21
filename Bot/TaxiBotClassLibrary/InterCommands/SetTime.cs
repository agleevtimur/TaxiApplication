using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TaxiBotClassLibrary.InterCommands
{
    public class SetTime : ICommand
    {
        public async void Execute(Message message, TelegramBotClient client)
        { 
            var id = message.From.Id;
            if (Check(message))
            {
                await client.SendTextMessageAsync(id, "Откуда едете?");
                var nextState = NDFAutomate<ICommand>.CurrentState.Transition[NDFAutomate<ICommand>.Functions[0]];
                nextState.Container = message.Text;
                NDFAutomate<ICommand>.CurrentState = nextState;
            }
            else
            {
                await client.SendTextMessageAsync(id, "Данные неккоретны \nПопробуйте еще раз");
            }

        }

        public bool Check(Message message)
        {
            bool formatIsCorrect = TimeSpan.TryParse(message.Text, out TimeSpan time);
            return formatIsCorrect;
            //var data = message.Text.Split(':');
            //bool hourIsValid;
            //bool minIsValid;
            //int minutes;
            //int hours;
            //if (data[0][0] == '0')
            //{
            //    hours = (int)Char.GetNumericValue(data[0][1]);
            //    hourIsValid = (hours > 0 && hours < 10);
            //}
            //else hourIsValid = int.TryParse(data[0], out hours) && hours < 24 && hours > 9;
            //if (data[1][0] == '0')
            //{
            //    minutes = (int)Char.GetNumericValue(data[1][1]);
            //    minIsValid = (minutes > 0 && minutes < 10);
            //}
            //else minIsValid = int.TryParse(data[0], out minutes) && minutes < 60 && minutes > 9;
            //return minIsValid && hourIsValid;

        }
    }
}
