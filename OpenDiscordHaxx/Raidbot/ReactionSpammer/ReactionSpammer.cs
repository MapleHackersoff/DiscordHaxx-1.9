using Discord;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiscordHaxx
{
    public class ReactionSpammer : RaidBot
    {
        private readonly ReactionsRequest _request;


        public ReactionSpammer(ReactionsRequest request)
        {
            Attack = new Attack(this) { Type = "Reaction spammer", Bots = Server.Bots.Count };

            Threads = request.Threads;
            _request = request;

            if (_request.ChannelId <= 0)
                throw new CheckException("Invalid channel ID");
            if (_request.MessageId <= 0)
                throw new CheckException("Invalid message ID");
            if (string.IsNullOrWhiteSpace(_request.Reaction))
                throw new CheckException("Invalid emoji");
        }


        public override void Start()
        {
            Parallel.ForEach(new List<RaidBotClient>(Server.Bots), new ParallelOptions() { MaxDegreeOfParallelism = Threads }, bot => 
            {
                if (ShouldStop)
                    return;

                try
                {
                    if (_request.Add)
                        bot.Client.AddMessageReaction(_request.ChannelId, _request.MessageId, _request.Reaction);
                    else
                        bot.Client.RemoveMessageReaction(_request.ChannelId, _request.MessageId, _request.Reaction);
                }
                catch (DiscordHttpException e)
                {
                    switch (e.Code)
                    {
                        case DiscordError.UnknownMessage:
                            Console.WriteLine($"[ERROR] Unknown message");
                            break;
                        case DiscordError.UnknownEmoji:
                            Console.WriteLine($"[ERROR] Unknown emoji");
                            break;
                        default:
                            CheckError(e);
                            break;
                    }
                }
                catch (RateLimitException) { }
            });

            Server.OngoingAttacks.Remove(Attack);
        }
    }
}
