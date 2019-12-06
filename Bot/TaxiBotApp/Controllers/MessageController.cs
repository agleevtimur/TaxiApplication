
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TaxiBotApp.Models;
using Telegram.Bot.Types;

namespace TaxiBotApp.Controllers
{
    [Route(@"api/message/update")] //webhook uri part
    public class MessageController : Controller
    {
        [HttpGet]
        public string Get()
        {
            return "taxi bot";
        }
        [HttpPost]
        public async Task<OkResult> Post([FromBody]Update update)
        {
            var message = update.Message;
            var client = await Bot.GetBot();

            foreach (var command in Bot.Commands)
            {
                if (command.Contains(message.Text))
                {
                    command.Execute(message, client);
                    break;
                }
            }
            return Ok();
        }

    }
}
