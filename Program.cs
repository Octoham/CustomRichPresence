using System;
using Discord;
using DiscordRichPresence;

namespace DiscordRichPresence
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Arguments: \n" +
                    "Client ID aka Application ID\n" +
                    "State\n" +
                    "Details\n" +
                    "Start Timestamp\n" +
                    "End Timestamp\n" +
                    "Large Image Key\n" +
                    "Large Image Text\n" +
                    "Small Image Key\n" +
                    "Small Image Text\n" +
                    "Party ID\n" +
                    "Party Size\n" +
                    "Party Max\n" +
                    "Join Secret");
                return;
            }
#if DEBUG
            System.Environment.SetEnvironmentVariable("DISCORD_INSTANCE_ID", "1");
#endif
            var discord = new Discord.Discord(long.Parse(args[0]), (UInt64)Discord.CreateFlags.Default);
            Console.WriteLine("Discord.Discord created");

            discord.SetLogHook(Discord.LogLevel.Debug, LogProblemsFunction);
            Console.WriteLine("Error logging started");

            var activityManager = discord.GetActivityManager();
            Console.WriteLine("Activity manager obtained");

            Discord.Activity activity;
            activity = makeActivity(args);
            Console.WriteLine("Activity created");

            activityManager.UpdateActivity(activity,(res) => { if (res == Discord.Result.Ok) { Console.WriteLine("Discord Result OK!"); } else { Console.WriteLine("Discord Result NOT OK!"); } });
            for(; ; )
            {
                discord.RunCallbacks();
            }

            
        }
        static public void LogProblemsFunction(Discord.LogLevel level, string message)
        {
            Console.WriteLine("Discord:{0} - {1}", level, message);
        }
        static public Discord.Activity makeActivity(string[] args)
        {
            Discord.Activity activity = new Discord.Activity();
            if (args.Length >= 2 && args[1] != "") 
            {
                activity.State = args[1];
            }
            if (args.Length >= 3 && args[2] != "") 
            {
                activity.Details = args[2];
            }
            if (args.Length >= 4 && args[3] != "")
            {
                activity.Timestamps.Start = long.Parse(args[3]);
            }
            if (args.Length >= 5 && args[4] != "")
            {
                activity.Timestamps.End = long.Parse(args[4]);
            }
            if (args.Length >= 6 && args[5] != "")
            {
                activity.Assets.LargeImage = args[5];
            }
            if (args.Length >= 7 && args[6] != "")
            {
                activity.Assets.LargeText = args[6];
            }
            if (args.Length >= 8 && args[7] != "")
            {
                activity.Assets.SmallImage = args[7];
            }
            if (args.Length >= 9 && args[8] != "")
            {
                activity.Assets.SmallText = args[8];
            }
            if (args.Length >= 10 && args[9] != "")
            {
                activity.Party.Id = args[9];
            }
            if (args.Length >= 11 && args[10] != "")
            {
                activity.Party.Size.CurrentSize = int.Parse(args[10]);
            }
            if (args.Length >= 12 && args[11] != "")
            {
                activity.Party.Size.MaxSize = int.Parse(args[11]);
            }
            if (args.Length >= 13 && args[12] != "")
            {
                activity.Secrets.Join = args[12];
            }
            return activity;
        }
    }
}