using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TaxiBotClassLibrary.InterCommands
{
    class SetAmount: ICommand
    {
        public async void Execute(Message message, TelegramBotClient client)
        {
            var id = message.From.Id;
            if (Check(message.Text))
            {
                await client.SendTextMessageAsync(id, "Поиск начался!");
                Configurator.Values.Add(message.Text);

                var data = new List<string>();
                var user = new DataBase.Classes.Client(message.From.Id, message.From.Username);//TODO Find должен возвращать TelegramId CHECK
                var t = Taxi_Algorithm.Algorithm.Find(Configurator.Values[0], Configurator.Values[1], Configurator.Values[2], Configurator.Values[3], user);
                Configurator.Values = new List<string>();

                if (t == null)
                    await client.SendTextMessageAsync(id, "Запрос обрабатывается");
                else
                    foreach (var mes in t)
                    {
                        DataBase.Classes.Request[] arr = { mes };
                        var otherUsers = t.Except(arr).Select(x =>'@' + x.Nickname);
                        var builder = new StringBuilder();//3 other users nickname
                        foreach (var item in otherUsers)
                        {
                            builder.Append(item);
                            if (item != otherUsers.Last())
                                builder.Append(", ");
                        }
                        await client.SendTextMessageAsync(mes.Telegram, builder.ToString());
                    }

            }
            else await client.SendTextMessageAsync(id, "Данные неккоретны \nПопробуйте еще раз");
        }

        public bool Check(string message)
        {
            if (int.TryParse(message, out int location))
            {
                return location > 0 && location < 4;
            }
            else return false;
        }
    }
}
