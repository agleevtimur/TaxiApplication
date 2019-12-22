using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TaxiBotClassLibrary.InterCommands
{
    public interface ICommand
    {
        void Execute(Message message, TelegramBotClient client);

        bool Check(string message);
    }
}
