using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;

namespace ConsoleApp1.Commands
{
    public class CallCommands : CallBase
    {
        protected override string GetDescription()
        {
            return "List all commands.";
        }

        public CallCommands()
        {
            Response = "Commands:\n\n";
            
            string[] commandsRaw = Directory.GetFiles("../../../Commands");
            string[] commands = new string[commandsRaw.Length];
            string[] commandDetails = new string[commandsRaw.Length];
            for (int i = 0; i < commands.Length; i++)
            {
                commands[i] = Regex.Split(commandsRaw[i].Split("\\").Last().Split(".").First(), @"(?<!^)(?=[A-Z])")[1];
                
                // What was I thinking?
                
                // Type t = Type.GetType($"Call{commands[i]}");
                // Debug.Assert(t != null, nameof(t) + " != null");
                // MethodInfo method = t.GetMethod("GetDescription",
                //     BindingFlags.Static | BindingFlags.Public);
                // commandDetails[i] = (string) method.Invoke(null, null);

                commandDetails[i] = GetDescription();
                Response += $"{commands[i]} - {commandDetails[i]}\n\n";
            }
        }
    }
}