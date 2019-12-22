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
        public async void Execute(Message message, TelegramBotClient Tclient)
        {
            var id = message.From.Id;
            if (Check(message.Text))
            {
                await Tclient.SendTextMessageAsync(id, "Поиск начался!");
                Configurator.Values.Add(message.Text);

                await Tclient.SendTextMessageAsync(id, $"{Configurator.Values[0]} + {Configurator.Values[1]} + {Configurator.Values[2]} + {Configurator.Values[3]}") ;
                var user = new DataBase.Classes.Client(message.From.Id, message.From.Username);//TODO Find должен возвращать TelegramId CHECK
                var clientsCompleted = Taxi_Algorithm.Algorithm.Find(Configurator.Values[0], Configurator.Values[1], Configurator.Values[2], Configurator.Values[3], user);
                Configurator.Values = new List<string>();

                if (clientsCompleted == null)
                    await Tclient.SendTextMessageAsync(id, "Запрос обрабатывается");
                else
                    foreach (var client in clientsCompleted)
                    {
                        DataBase.Classes.Client[] arr = { client};
                        var otherUsers = clientsCompleted.Except(arr).Select(x =>'@' + x.Nickname);
                        var builder = new StringBuilder();//3 other users nickname
                        foreach (var item in otherUsers)
                        {
                            builder.Append(item);
                            if (item != otherUsers.Last())
                                builder.Append(", ");
                        }
                        await Tclient.SendTextMessageAsync(client.Telegram, builder.ToString());
                        
                    }
                StateMachine.Revoke();
                //Configurator.Dict[id] = NDFAutomate<ICommand>.States[0];
                

            }
            else await Tclient.SendTextMessageAsync(id, "Данные неккоретны \nПопробуйте еще раз");
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
