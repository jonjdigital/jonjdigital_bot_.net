using System;
using DSharpPlus.Entities;
using MySql.Data.MySqlClient;

namespace ConsoleApp1
{
    public class Functions
    {

        private int modRole = 3;
        private int adminRole = 2;

        private static string conString = BotSetttings.con_string;
        private MySqlConnection con = new MySqlConnection(conString);

        public int leveling(DiscordMessage msg)
        {
            // levelling exp: msg.content.Length() * 0.5
            // var levelBoundary = (((currLevel * 20) * currLevel * 0.8) + currLevel * 100) - 16;

            DiscordGuild guild = msg.Channel.Guild;
            DiscordUser author = msg.Author;

            //see if user has any levels
            con.Open();
            string stm = $"Select * from levels where user_id = {author.Id} and server_id = {guild.Id}";
            var userCmd = new MySqlCommand(stm, con);

            MySqlDataReader userRdr = userCmd.ExecuteReader();

            if (userRdr.Read())
            {
                //user has levels
                con.Close();

                int currExp = userRdr.GetInt32(2);
                int currLvl = userRdr.GetInt32(3);
                int ogLvl = currLvl;

                //get exp from msg
                int exp = msg.Content.Length;
                int totalExp = currExp + exp;

                //will exp push past next level boundary
                while (totalExp >= ((((currLvl * 20) * currLvl * 0.8) + currLvl * 100) - 16))
                {
                    //increase level
                    currLvl++;
                }

                // update record with new exp count
                userRdr.Close();

                string updateStm =
                    $"update levels set(current_exp, current_level) values({totalExp},{currLvl}) where user_id = {author.Id} and server_id = {guild.Id}";
                con.Open();
                MySqlCommand updateCmd = new MySqlCommand(updateStm, con);
                MySqlDataReader updated = updateCmd.ExecuteReader();

                if (updated.RecordsAffected != 0)
                {
                    if (currLvl != ogLvl)
                    {
                        return currLvl;
                    }

                    return 0;
                }

                return -1;
            }

            //user hasnt got any levels yet
            con.Close();

            int currLevel = 0;
            int ttlExp = msg.Content.Length;

            while (ttlExp >= ((((currLevel * 20) * currLevel * 0.8) + currLevel * 100) - 16))
            {
                //increase level
                currLevel++;
            }

            int ogLevel = 0;
            // if (ogLevel != currLevel)
            // {
            //     Console.WriteLine("Current Level: " + currLevel);
            // }

            return currLevel;
        }

        public void initateLevels(DiscordGuild guild, DiscordMember member)
        {
            //see if user has any levels
            con.Open();
            string stm = $"Select * from levels where user_id = {member.Id} and server_id = {guild.Id}";
            var userCmd = new MySqlCommand(stm, con);

            MySqlDataReader userRdr = userCmd.ExecuteReader();

            if (userRdr.Read())
            {
                //user hasnt got any levels yet
                con.Close();
                userRdr.Close();
                con.Open();
                string insert =
                    $"insert into levels (user_id,server_id,current_exp,current_level) values({member.Id},{guild.Id},0,0)";
                MySqlCommand cmd = new MySqlCommand(insert, con);
                
                //check if user has been initiated successfully
                MySqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.RecordsAffected != 0)
                {
                    Console.WriteLine(member.Username + "#" + member.Discriminator + " has been initiated for " + guild.Name);
                }
                else
                {
                    Console.WriteLine(member.Username + "#" + member.Discriminator + " could not be initiated for " + guild.Name);
                }
            }
        }
    }
}
