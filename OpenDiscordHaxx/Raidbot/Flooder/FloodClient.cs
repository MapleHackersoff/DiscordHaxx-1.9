using System;
using Discord;

namespace DiscordHaxx
{
    public class FloodClient
    {
        private readonly DiscordClient _client;
        private readonly ulong _channelId;


        public FloodClient(DiscordClient client, ulong id, bool dm)
        {
            _client = client;
            _channelId = dm ? _client.CreateDM(id).Id : id;
        }


        public bool TrySendMessage(string message, Embed embed)
        {
            try
            {
                _client.SendMessage(_channelId, message, false, embed);
            }
            catch (DiscordHttpException e)
            {
                if (e.Code == DiscordError.ChannelVerificationTooHigh)
                    Console.WriteLine("[ERROR] channel verification too high");
                else
                    RaidBot.CheckError(e);

                return false;
            }
            catch (RateLimitException) { }


            return true;
        }
    }
}
