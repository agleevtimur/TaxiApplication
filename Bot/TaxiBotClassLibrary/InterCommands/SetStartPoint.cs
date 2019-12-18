﻿using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TaxiBotClassLibrary.InterCommands
{
    class SetStartPoint : ICommand
    {
        public async void Execute(Message message, TelegramBotClient client)
        {
            
            var id = message.From.Id;
            if (Check(message))
            {
                await client.SendTextMessageAsync(id, "Куда едете?");
                var nextState = NDFAutomate<ICommand>.CurrentState.Transition[NDFAutomate<ICommand>.Functions[1]];
                nextState.Container = message.Text;
                NDFAutomate<ICommand>.CurrentState = nextState;
            }
            else await client.SendTextMessageAsync(id, "Данные неккоретны \nПопробуйте еще раз");
        }
        public bool Check(Message message)
        {
            if (int.TryParse(message.Text, out int location))
            {
                return location > 0 && location < 8;
            }
            else return false;
        }
    }
}
