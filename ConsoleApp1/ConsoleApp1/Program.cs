using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Console.WriteLine("Hello World!");
            MainAsync().GetAwaiter().GetResult();
        }

        static async Task MainAsync()
        {
            string token = "NzI5MDY2NDE2ODc0OTc5NDA5.XwDiAw.ktVq4pO_76JIL1no9E8IqUCJ82A";
            string prefix = "!";

            var discord = new DiscordClient(new DiscordConfiguration()
            {
                Token = token,
                TokenType = TokenType.Bot
            });

            discord.MessageCreated += async (s, e) =>
            {
                
                DiscordMessage msg = e.Message;
                DiscordUser author = e.Message.Author;
                DiscordMember authorMember = e.Message.Channel.Guild.GetMemberAsync(author.Id).Result;

                if (author.IsBot) return;

                if (msg.Content.ToLower() == "hi")
                {
                    await msg.RespondAsync($"<@{author.Id}> Hi!");
                }
            };
            
            await discord.ConnectAsync();
            await Task.Delay(-1);
        }
    }
}