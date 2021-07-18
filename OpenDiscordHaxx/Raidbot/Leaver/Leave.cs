using Newtonsoft.Json;

namespace DiscordHaxx
{
    public class LeaveRequest : RaidRequest
    {
        [JsonProperty("guild_id")]
        public ulong GuildId { get; private set; }


        [JsonProperty("group")]
        public bool Group { get; private set; }
    }
}
