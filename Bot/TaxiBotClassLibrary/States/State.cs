using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TaxiBotClassLibrary.States
{
    abstract public class State
    {
        protected Context context;

        public void SetContext(Context context)
        {
            this.context = context;
        }
        // исполняющий метод
        public abstract void HandleForward(Message message, TelegramBotClient client);
        // метод отката состояния
        public abstract void HandleBack(int id, TelegramBotClient client);
        // проверка на корректность входных данных
        public abstract bool Check(string message);
    }
}
