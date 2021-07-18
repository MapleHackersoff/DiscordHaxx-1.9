using Newtonsoft.Json;

namespace DiscordHaxx
{
    class BotInfoRequest : BotRequest
    {
        public BotInfoRequest() : base(ListOpcode.BotInfo)
        { }


        [JsonProperty("id")]
        public ulong Id { get; private set; }


        [JsonProperty("get_guilds_and_friends")]
        public bool GetGuildsAndFriends { get; private set; }
    }
}
