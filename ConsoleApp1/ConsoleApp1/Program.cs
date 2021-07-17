using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using ConsoleApp1;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            DotEnv.Load(".env");
            MainAsync().GetAwaiter().GetResult();
        }

        static async Task MainAsync()
        {
            string token = Environment.GetEnvironmentVariable("token");
            string prefix = "!";
            string botVersion = "1.0.0"; // x.y.z - x = Major Feature Update, y = Minor Feature Update, 

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