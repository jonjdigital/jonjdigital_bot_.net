using System;
using DSharpPlus;
using DSharpPlus.Entities;

namespace ConsoleApp1.Commands
{
    public class CallStats : CallBase
    {
        protected override string GetDescription()
        {
            return "Gets public stats for the server";
        }

        public CallStats(DiscordGuild guild, DiscordUser author)
        {
            
            //get guild owner
            DiscordMember owner = guild.Owner;
            //get server creation date
            

            DiscordEmbedBuilder emb = new DiscordEmbedBuilder()
            {
                Title = $"Stats for {guild.Name}",
                Color = owner.Color,
                Footer = new DiscordEmbedBuilder.EmbedFooter()
                {
                    Text = author.Username + "#" + author.Discriminator,
                    IconUrl = author.AvatarUrl,
                },
            };
            emb.AddField("Owner: ",owner.Username + "#" + owner.Discriminator + "\n");
            emb.AddField("Member Count: ",guild.MemberCount.ToString());
            emb.AddField("Server Created: ",guild.CreationTimestamp.ToString());

            Emb = emb;
        }
    }
}