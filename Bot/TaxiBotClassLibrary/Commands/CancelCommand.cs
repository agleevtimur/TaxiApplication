
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TaxiBotClassLibrary.Commands
{
    class CancelCommand : Command
    {
        public override string Name => "/cancel";

        public override async void Execute(Message message, TelegramBotClient client)
        {
            var id = message.From.Id;
            Taxi_Algorithm.Algorithm.DeleteCanceledRequest(new DataBase.Classes.Client(message.From.Id, message.From.Username));
            if (Configurator.Dictionary.ContainsKey(id))
                Configurator.Dictionary[id] = null;// обнуляем
            await client.SendTextMessageAsync(id, "Вы прекратили поиск");
        }

    }
}
