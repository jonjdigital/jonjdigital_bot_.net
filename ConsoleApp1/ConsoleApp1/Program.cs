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
            MainAsync().GetAwaiter().GetResult();
        }

        static async Task MainAsync()
        {
            DotEnv.Load("../../../.env");
            string token = Environment.GetEnvironmentVariable("token");
            string prefix = "!";
            string adminPrefix = "?";
            string botVersion = "1.0.0"; // x.y.z
                                         // x = Major Feature Update
                                         // y = Minor Feature Update & Major Bug Update
                                         // z = Minor Updates

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

                //if message is from the bot, we will ignore it
                if (author.IsBot) return;

                //if the user just types hi, the bot will reply to the user!
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