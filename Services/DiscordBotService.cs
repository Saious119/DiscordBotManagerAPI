using DiscordBotManagerAPI.Models;
using System.ComponentModel;

namespace DiscordBotManagerAPI.Services
{
    public class DiscordBotService : IDiscordBotService
    {
        public List<BotProcess> botList = new();
        public async Task StartBot(string dir, string botName, string cmd)
        {
            if (IsBotRunning(botName))
            {
                Console.WriteLine("Bot is already running");
                return;
            }
            System.Diagnostics.ProcessStartInfo procStartInfo;
            if (String.IsNullOrEmpty(cmd))
            {
                procStartInfo = new System.Diagnostics.ProcessStartInfo("./", $"{botName}");
            }
            else
            {
                procStartInfo = new System.Diagnostics.ProcessStartInfo("/bin/bash", $"{cmd} {botName}");
            }
            procStartInfo.WorkingDirectory = "/home/pi/Discord-Bots/" + dir;
            procStartInfo.RedirectStandardOutput = true;
            procStartInfo.RedirectStandardError = true;
            procStartInfo.UseShellExecute = false;

            procStartInfo.CreateNoWindow = true;

            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo = procStartInfo;

            if (botList.Find(x => x.botName == botName) != null)
            {
                BotProcess bot = new(botName, proc); //create an object to store where this proc is
                botList.Add(bot);
            }
            botList.Find(x => x.botName == botName).process.Start(); //start the process in the array
            botList.Find(x => x.botName == botName).running = true; //if the previous line didn't fail then it is running
            botList.Find(x => x.botName == botName).process.WaitForExit();
            botList.Find(x => x.botName == botName).running = false; //when the process exits, the bot is no longer running
        }
        public async Task KillBot(string botName)
        {
            if (!IsBotRunning(botName))
            {
                Console.WriteLine("Bot is not running");
                return;
            }
            botList.Find(x => x.botName == botName).process?.Kill();
            botList.Find(x => x.botName == botName).running = false;
            botList.Remove(botList.Find(x => x.botName == botName));
        }
        public bool IsBotRunning(string botName)
        {
            var bot = botList.Find(x => x.botName == botName);
            if(bot == null) return false;
            return bot.running;
        }
        public async Task<string> Status(string botName)
        {
            var bot = botList.Find(x => x.botName == botName);
            if(bot == null || bot?.running == false)
            {
                return "dead";
            }
            else
            {
                return "running";
            }
        }
    }
}
