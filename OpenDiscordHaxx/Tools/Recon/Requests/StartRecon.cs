using Newtonsoft.Json;

namespace DiscordHaxx
{
    public class StartReconRequest : ReconRequest
    {
        public StartReconRequest() : base(0, ReconOpcode.StartRecon)
        { }


        [JsonProperty("guild_id")]
        public ulong GuildId { get; private set; }
    }
}
