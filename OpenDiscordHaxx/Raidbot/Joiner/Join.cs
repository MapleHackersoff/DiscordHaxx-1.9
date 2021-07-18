using Newtonsoft.Json;

namespace DiscordHaxx
{
    public class JoinRequest : RaidRequest
    {
        [JsonProperty("invite")]
        public string Invite { get; private set; }
    }
}
