
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TaxiBotApp.Models;
using TaxiBotClassLibrary;
using Telegram.Bot.Types;

namespace TaxiBotApp.Controllers
{
    [Route(@"api/message/update")] //webhook uri part
    public class MessageController : Controller
    {

        [HttpPost]
        public async Task<OkResult> Post([FromBody]Update update)
        {
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
            else
            {
               foreach (var command in Bot.InternalCommands)
                {
                    command.Execute(message, client);
                }
            }
            return Ok();
        }

    }
}
