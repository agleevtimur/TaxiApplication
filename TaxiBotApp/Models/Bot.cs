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
        static List<Command> commands;

        public static IReadOnlyList<Command> Commands => commands.AsReadOnly();
        
        public static async Task<TelegramBotClient> GetBot()
        {
            if (client != null)
            {
                return client;
            }
            commands = new List<Command>();
            commands.Add(new HelloCommand());
            client = new TelegramBotClient(AppSettings.Key);
            var hook = string.Format(AppSettings.Url, @"api/message/update");
            await client.SetWebhookAsync(hook);

            return client;
        }
    }
}
