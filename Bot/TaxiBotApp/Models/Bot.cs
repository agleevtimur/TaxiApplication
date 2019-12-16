using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaxiBotClassLibrary;
using TaxiBotClassLibrary.Commands;
using TaxiBotClassLibrary.InterCommands;
using Telegram.Bot;

namespace TaxiBotApp.Models
{
    public static class Bot
    {
        static TelegramBotClient client;
        public static IReadOnlyList<Command> Commands => Configurator.GetCommands().AsReadOnly();
        public static IReadOnlyList<InterCommand> InternalCommands => Configurator.GetInternalCommand().AsReadOnly();
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
