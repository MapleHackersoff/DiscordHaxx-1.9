using Discord;
using System;
using System.Linq;

namespace DiscordHaxx
{
    public abstract class RaidBot
    {
        public int Threads { get; protected set; }
        public bool ShouldStop { get; set; }
        public Attack Attack { get; protected set; }


        public abstract void Start();


        public static void CheckError(DiscordHttpException ex)
        {
            switch (ex.Code)
            {
                case DiscordError.UnknownChannel:
                    Console.WriteLine($"[ERROR] Unknown channel");
                    break;
                case DiscordError.UnknownGuild:
                    Console.WriteLine("[ERROR] invalid guild");
                    break;
                case DiscordError.AccountUnverified:
                    Console.WriteLine($"[ERROR] {ex.Client.User} is unverified and has been removed from the bot list");

                    try
                    {
                        Server.Bots.Remove(Server.Bots.First(bot => bot.Client.User.Id == ex.Client.User.Id));
                    }
                    catch { }
                    break;
                default:
                    Console.WriteLine($"[ERROR] Unknown: {ex.Code} | {ex.ErrorMessage}");
                    break;
            }
        }
    }
}
