using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;

namespace ConsoleApp1.Commands
{
    public class CallCommands : CallBase
    {
        protected override string GetDescription(MessageCreateEventArgs e)
        {
            return "List all commands.";
        }

        public CallCommands()
        {
            Response = "Commands:\n\n";
            
            string[] commandsRaw = Directory.GetFiles("../");
            string[] commands = new string[commandsRaw.Length];
            string[] commandDetails = new string[commandsRaw.Length];
            for (int i = 0; i < commands.Length; i++)
            {
                commands[i] = Regex.Split(commandsRaw[i], @"(?<!^)(?=[A-Z])")[0];
                Type t = Type.GetType(commands[i]);
                Debug.Assert(t != null, nameof(t) + " != null");
                MethodInfo method = t.GetMethod("GetDescription",
                    BindingFlags.Static | BindingFlags.Public);
                commandDetails[i] = (string) method.Invoke(null, null);
                Response += $"{commands[i]} - {commandDetails[i]}\n\n";
            }
        }
    }
}