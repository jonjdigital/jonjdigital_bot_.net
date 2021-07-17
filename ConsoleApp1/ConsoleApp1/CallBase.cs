using DSharpPlus.EventArgs;

namespace ConsoleApp1
{
    public abstract class CallBase
    {
        public string Response = "";
        protected abstract string GetDescription(MessageCreateEventArgs e);
    }
}