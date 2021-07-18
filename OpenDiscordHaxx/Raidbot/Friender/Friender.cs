using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord;

namespace DiscordHaxx
{
    public class Friender : RaidBot
    {
        private readonly FriendRequest _request;


        public Friender(FriendRequest request)
        {
            Attack = new Attack(this) { Type = "Friender", Bots = Server.Bots.Count };

            Threads = request.Threads;
            _request = request;

            if (string.IsNullOrWhiteSpace(_request.Username))
                throw new CheckException("Please enter a username");
            if (_request.Discriminator < 1)
                throw new CheckException("Invalid discriminator");
        }


        public override void Start()
        {
            Parallel.ForEach(new List<RaidBotClient>(Server.Bots), new ParallelOptions() { MaxDegreeOfParallelism = _request.Threads }, bot =>
            {
                if (ShouldStop)
                    return;

                try
                {
                    if (bot.SocketClient)
                    {
                        var results = bot.Relationships.Where(b => b.User.Username == _request.Username && b.User.Discriminator == _request.Discriminator).ToList();

                        if (results.Count > 0)
                        {
                            if (results[0].Type == RelationshipType.Friends || results[0].Type == RelationshipType.OutgoingRequest)
                                return;
                            else if (results[0].Type == RelationshipType.Blocked || results[0].Type == RelationshipType.IncomingRequest)
                                results[0].Remove();
                        }
                    }

                    bot.Client.SendFriendRequest(_request.Username, _request.Discriminator);
                }
                catch (DiscordHttpException e)
                {
                    if (e.Code == DiscordError.InvalidRecipient)
                        Console.WriteLine($"[ERROR] invalid recipient");
                    else
                        CheckError(e);
                }
                catch (RateLimitException) { }
            });

            Server.OngoingAttacks.Remove(Attack);
        }
    }
}
