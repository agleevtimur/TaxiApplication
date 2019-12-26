
using Telegram.Bot;
using Telegram.Bot.Types;
using TaxiBotClassLibrary.States;

namespace TaxiBotClassLibrary.Commands
{
    class FindCommand : Command
    {
        public override string Name => "/find";
        public override async void Execute(Message message, TelegramBotClient client)
        {
            var id = message.From.Id;
            await client.SendTextMessageAsync(id, "Давайте найдем для вас попутчиков");
            DictInitialize(id);// инициализируем словарь для пользователя
            await client.SendTextMessageAsync(id, "Введите удобное вам время для заказа такси в формате HH:MM");
        }
        private void DictInitialize(int id)
        {
            if (Configurator.Dictionary.ContainsKey(id))
                Configurator.Dictionary[id] = new Context(new StartState());
            else
                Configurator.Dictionary.Add(id, new Context(new StartState()));
            Configurator.Dictionary[id].Values = new string[4];
        }
    }
}
