using Newtonsoft.Json;

namespace DiscordHaxx
{
    public class ReactionsRequest : RaidRequest
    {
        [JsonProperty("channel_id")]
        public ulong ChannelId { get; private set; }


        [JsonProperty("message_id")]
        public ulong MessageId { get; private set; }


        [JsonProperty("reaction")]
        public string Reaction { get; private set; }


        [JsonProperty("add")]
        public bool Add { get; private set; }
    }
}
