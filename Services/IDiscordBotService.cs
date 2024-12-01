namespace DiscordBotManagerAPI.Services
{
    public interface IDiscordBotService
    {
        public Task StartBot(string dir, string botName, string cmd);
        public Task KillBot(string botName);
        public bool IsBotRunning(string botName);
        public Task<string> Status(string botName);

    }
}
