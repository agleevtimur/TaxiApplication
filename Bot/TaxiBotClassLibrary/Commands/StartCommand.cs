
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TaxiBotClassLibrary.Commands
{
    class StartCommand : Command
    {
        public override string Name => "/start";

        public override async void Execute(Message message, TelegramBotClient client)
        {
            var id = message.From.Id;
            var text = "Добро пожаловать в сервис поиска попутчиков такси!\n" +
                "Вам нравится кататься на такси, но не любите тратить много денег, а попутчиков бывает найти очень " +
                "проблематично?\n" + "Тогда этот бот именно для вас!\n" +
                "Для начала работы /find\n" +
                "Список доступных локаций /locations";
            await client.SendTextMessageAsync(id, text);
        }
    }
}
