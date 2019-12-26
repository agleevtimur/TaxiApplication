using System;
using System.Linq;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TaxiBotClassLibrary.States
{
    class FinishState : State
    {
        public override async void HandleForward(Message message, TelegramBotClient Tclient)
        {// сколько человек уже едет
            var id = message.From.Id;
            if (Check(message.Text))
            {
                await Tclient.SendTextMessageAsync(id, $"Поиск начался!");
                Configurator.Dictionary[id].Values[3] = message.Text;
                var values = Configurator.Dictionary[id].Values;

                InformOut(id, Tclient, values);// обобщаем запрос
                // получаем экземпляр пользователя через БД
                var user = new DataBase.Classes.Client(message.From.Id, message.From.Username);

                FindFellows(values, Tclient, user);
                
            }
            else await Tclient.SendTextMessageAsync(id, "Данные неккоретны \nПопробуйте еще раз");
        }

        private async void FindFellows(String[] values , TelegramBotClient Tclient, DataBase.Classes.Client user)
        {
            // алгоритм возвращает найденных попутчиков для каждого пользователя по его данным запроса
            var clientsCompleted = Taxi_Algorithm.Algorithm.Find(values[0], values[1], values[2], values[3], user);

            if (clientsCompleted == null)
            {// если не еще нашлись, то user переходит в состояние ожидания 
                await Tclient.SendTextMessageAsync(user.Telegram, "Запрос обрабатывается");
                Configurator.Dictionary[user.Telegram].TransitionTo(new AwaitState());
            }
            else// получили попутчиков, включая самого пользователя
                foreach (var client in clientsCompleted)
                {
                    DataBase.Classes.Client[] arr = { client };// получим только попутчиков
                    var otherUsers = clientsCompleted.Except(arr).Select(x => '@' + x.Nickname);
                    var builder = new StringBuilder();//other users nickname
                    foreach (var item in otherUsers)
                    {
                        builder.Append(item);
                        if (item != otherUsers.Last())
                            builder.Append(", ");
                    }
                    await Tclient.SendTextMessageAsync(client.Telegram, "Ваш(и) попутчик(и) " + builder.ToString());
                    Configurator.Dictionary[client.Telegram] = null;// обнулим всем состояния
                }
            
        }

        private async void InformOut(int id, TelegramBotClient client, string[] values)
        {
            var loc = Taxi_Algorithm.Algorithm.GetLocations();
            var information = $"Вы сделали запрос на {values[0]} от {loc[int.Parse(values[1])].NameOfPoint} " +
                $"до {loc[int.Parse(values[2])].NameOfPoint}, при этом имеете уже {int.Parse(values[3])} попутчик(ов)";
            await client.SendTextMessageAsync(id, information);
        }

        public override bool Check(string message)
        {
            if (int.TryParse(message, out int location))
            {
                return location > 0 && location < 4;
            }
            else return false;
        }

        public override async void HandleBack(int id, TelegramBotClient client)
        {
            Configurator.Dictionary[id].TransitionTo(new State2());
            await client.SendTextMessageAsync(id, "Куда едете?");
        }
    }
}
