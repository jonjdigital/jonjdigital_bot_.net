using DSharpPlus.Entities;
using DSharpPlus.EventArgs;

namespace ConsoleApp1
{
    public abstract class CallBase
    {
        public string Response = "";

        public DiscordEmbedBuilder Emb;
        protected abstract string GetDescription();
    }
}