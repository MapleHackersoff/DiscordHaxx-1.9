using Newtonsoft.Json;

namespace DiscordHaxx
{
    public class VCRequest : RaidRequest
    {
        [JsonProperty("guild_id")]
        public ulong GuildId { get; private set; }


        [JsonProperty("channel_id")]
        public ulong ChannelId { get; private set; }


        [JsonProperty("join")]
        public bool Join { get; private set; }


        [JsonProperty("delay")]
#pragma warning disable CS0649
        private readonly string _delay;
#pragma warning restore CS0649


        public int Delay
        {
            get { return string.IsNullOrWhiteSpace(_delay) ? 0 : int.Parse(_delay); }
        }
    }
}
