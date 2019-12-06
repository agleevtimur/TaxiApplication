using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaxiBotApp.Models.Commands;
using Telegram.Bot;

namespace TaxiBotApp.Models
{
    public static class Bot
    {
        static TelegramBotClient client;

        public static IReadOnlyList<Command> Commands => AppSettings.Commands.AsReadOnly();
        
            
        public static async Task<TelegramBotClient> GetBot()
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
