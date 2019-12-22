
using DataBase.Classes;
using DataBase.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
//using System.Web.Http;
//using System.Web.Http.Results;
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
            else if (message.Text != "")
            {
                Bot.InternalCommands.Execute(message, client);
                //if (state.Status == 0)

                //     await Bot.InternalCommands[0].Execute(message, client);
                //else
                //if (state.Status == 1)
                //    await Bot.InternalCommands[1].Execute(message, client);
                //else
                //if (state.Status == 2)
                //    await Bot.InternalCommands[2].Execute(message, client);
                //else
                //if (state.Status == 3)
                //   await Bot.InternalCommands[3].Execute(message, client);
            }
            return Ok();
        }

    }
}
