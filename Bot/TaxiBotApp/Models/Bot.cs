
using System.Collections.Generic;
using System.Threading.Tasks;
using TaxiBotClassLibrary;
using TaxiBotClassLibrary.Commands;
using Telegram.Bot;

namespace TaxiBotApp.Models
{
    public static class Bot
    {
        static TelegramBotClient client;
        public static IReadOnlyList<Command> Commands => Configurator.GetCommands().AsReadOnly();
        public static Dictionary<int, Context> InterStates => Configurator.Dictionary;                                                                         
        public static async Task<TelegramBotClient> GetMe()
        {
            if (client != null)
            {
                return client;
            }
            client = new TelegramBotClient(AppSettings.Key);
            var hook = string.Format(AppSettings.Url, @"api/message/update");
            await client.SetWebhookAsync(hook);

            return client;
        }
    }
}
