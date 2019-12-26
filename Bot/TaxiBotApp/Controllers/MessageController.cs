
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TaxiBotApp.Models;
using Telegram.Bot.Types;


namespace TaxiBotApp.Controllers
{
    [Route(@"api/message/update")]
    public class MessageController : Controller
    {
        [HttpPost]
        public async Task<OkResult> Post([FromBody]Update update)
        {// получаем уже сериализованный update от телеграм
            var message = update.Message;
            var client = await Bot.GetMe();

            if (message.Text.StartsWith('/'))
            {
                foreach (var command in Bot.Commands)
                {
                    if (command.Contains(message.Text))
                    {
                        command.Execute(message, client);
                        break;
                    }
                }
            }
            else if (Bot.InterStates[message.From.Id] != null)
                Bot.InterStates[message.From.Id].Request(message, client);
            else
                await client.SendTextMessageAsync(message.From.Id, "для начала работы /find");
            return Ok();
        }
    }
}
