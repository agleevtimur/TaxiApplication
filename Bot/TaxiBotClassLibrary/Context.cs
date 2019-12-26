using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaxiBotClassLibrary.States;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TaxiBotClassLibrary
{// паттерн состояние
    public class Context
    {
        // Ссылка на текущее состояние Контекста.
        private State state = null;
        public Context(State state)
        {
            this.TransitionTo(state);
        }
        // ссылка на след состояние
        public void TransitionTo(State state)
        {
            this.state = state;
            this.state.SetContext(this);
        }
        // Контекст делегирует часть своего поведения текущему объекту
        // Состояния.
        public void Request(Message message, TelegramBotClient client)
        {
            if (message.Text == "back")
                state.HandleBack(message.From.Id, client);
            else
                state.HandleForward(message, client);
        }
        public string[] Values = null;// хранятся данные запроса каждого пользователя
        // их всего 4 - время; откуда; куда; сколько человек уже есть
    }
}
