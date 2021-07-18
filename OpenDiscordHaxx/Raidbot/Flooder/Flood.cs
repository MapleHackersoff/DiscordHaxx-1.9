using Discord;
using Newtonsoft.Json;

namespace DiscordHaxx
{
    public class FloodRequest : RaidRequest
    {
        [JsonProperty("channel_id")]
        public ulong ChannelId { get; private set; }


        [JsonProperty("message")]
        public string Message { get; private set; }


        [JsonProperty("dm")]
        public bool DM { get; private set; }


        [JsonProperty("mass_mention")]
        public bool MassMention { get; private set; }


        [JsonProperty("use_embed")]
        public bool UseEmbed { get; private set; }


        [JsonProperty("embed")]
        public Embed Embed { get; private set; }
    }
}
