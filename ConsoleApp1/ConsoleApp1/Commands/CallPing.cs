using DSharpPlus;
using DSharpPlus.Entities;

namespace ConsoleApp1.Commands
{
    public class CallPing : CallBase
    {
        protected override string GetDescription()
        {
            return "Gives the bots latency in milliseconds";
        }

        public CallPing(DiscordClient client)
        {
            Response = client.Ping + "ms";
        }
    }
}