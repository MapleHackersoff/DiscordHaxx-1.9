using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Discord;

namespace DiscordHaxx
{
    public class Leaver : RaidBot
    {
        private readonly ulong _guildId;
        private readonly bool _group;

        public Leaver(LeaveRequest request)
        {
            Attack = new Attack(this) { Type = "Leaver", Bots = Server.Bots.Count };

            Threads = request.Threads;
            _guildId = request.GuildId;
            _group = request.Group;

            if (_guildId <= 0)
                throw new CheckException("Invalid guild ID");
        }


        public override void Start()
        {
            Parallel.ForEach(new List<RaidBotClient>(Server.Bots), new ParallelOptions() { MaxDegreeOfParallelism = Threads }, bot =>
            {
                try
                {
                    if (ShouldStop)
                        return;

                    if (_group)
                        bot.Client.LeaveGroup(_guildId);
                    else
                        bot.Client.LeaveGuild(_guildId);
                }
                catch (DiscordHttpException e)
                {
                    CheckError(e);
                }
                catch (RateLimitException) { }
            });

            Server.OngoingAttacks.Remove(Attack);
        }
    }
}
