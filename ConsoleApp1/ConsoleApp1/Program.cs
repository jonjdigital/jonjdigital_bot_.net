using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using ConsoleApp1;
using ConsoleApp1.Commands;

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
            // string token = Environment.GetEnvironmentVariable("token");
            string token = BotSetttings.token;
            string prefix = "!";
            string adminPrefix = "?";
            string sourceVersion = "1.0.0"; // x.y.z
                                         // x = Major Feature Update
                                         // y = Minor Feature Update & Major Bug Update
                                         // z = Minor Updates

            var discord = new DiscordClient(new DiscordConfiguration()
            {
                Token = token,
                TokenType = TokenType.Bot
            });

            discord.GuildMemberAdded += (s, e) =>
            {
                Functions func = new Functions();
                func.initateLevels(e.Guild, e.Member);
                Console.WriteLine(e.Guild.Name);
                return null;
            };

            discord.MessageCreated += async (s, e) =>
            {
                Functions functions = new Functions();
                DiscordMessage msg = e.Message;
                DiscordUser author = e.Message.Author;
                DiscordGuild guild = msg.Channel.Guild;
                DiscordMember authorMember = e.Message.Channel.Guild.GetMemberAsync(author.Id).Result;

                //if message is from the bot, we will ignore it
                if (author.IsBot) return;
                
                //run the levelling command
                Functions funct = new Functions();
                funct.leveling(msg);

                CallBase caller;
                string response;

                
                //admin based commands
                if (msg.Content.ToLower() == adminPrefix + "ping")
                {
                    caller = new CallPing(discord);
                    response = caller.Response;
                    await msg.RespondAsync(response);
                }
                
                //public available commands
                if (msg.Content.ToLower() == prefix + "commands")
                {
                    caller = new CallCommands();
                    response = caller.Response;
                    await msg.RespondAsync(response);
                }

                if (msg.Content.ToLower() == prefix + "stats")
                {
                    caller = new CallStats(guild, author);
                    DiscordEmbed embed = caller.Emb.Build();
                    await msg.RespondAsync(embed);
                }

            };

            
            
            await discord.ConnectAsync();
            await Task.Delay(-1);
        }
    }
}