﻿using System;
using System.Collections.Generic;
using System.Linq;
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
            var user = new DataBase.Classes.User(message.From.Username, id);
            var t = Taxi_Algorithm.Algorithm.Find(data[0], data[1], data[2], data[3], user);
            if (t == null)
                await client.SendTextMessageAsync(id, "Запрос обрабатывается");
            else 
                foreach(var mes in t)
                {
                    DataBase.Classes.User[] arr = { mes };
                    var otherUsers = t.Except(arr).Select(x => x.Nickname);
                    var builder = new StringBuilder();//3 other users nickname
                    foreach(var item in otherUsers)
                    {
                        builder.Append(item);
                        if(item!=otherUsers.Last())
                        builder.Append(", ");
                    }
                    await client.SendTextMessageAsync(mes.Id, builder.ToString());
                }
        }
    }
}