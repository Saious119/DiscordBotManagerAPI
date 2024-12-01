using DiscordBotManagerAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace DiscordBotManagerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BotController : ControllerBase
    {
        private readonly IDiscordBotService _botService;

        public BotController(IDiscordBotService botService)
        {
            _botService = botService;
        }

        [HttpPost]
        [Route("StartBot")]
        public IActionResult StartNodeBot(string dir, string botName, string cmd)
        {
            try
            {
                _botService.StartBot(dir, botName, cmd);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, ex);
            }
            return Ok();
        }

        [HttpPost]
        [Route("KillBot")]
        public IActionResult KillBot(string botName)
        {
            try
            {
                _botService.KillBot(botName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, ex);
            }
            return Ok();
        }
        [HttpGet]
        [Route("GetStatus")]
        public async Task<IActionResult> GetBot(string botName)
        {
            try
            {
                var status = _botService.Status(botName);
                return Ok(status);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, ex);
            }
        }
    }
}
