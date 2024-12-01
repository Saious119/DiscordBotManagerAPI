using System.Diagnostics;

namespace DiscordBotManagerAPI.Models
{
    public class BotProcess
    {
        public string botName { get; set; }
        public Process process { get; set; }
        public bool running { get; set; }

        public BotProcess(string botName, Process process)
        {
            this.botName = botName;
            this.process = process;
            running = false;
        }
    }
}
