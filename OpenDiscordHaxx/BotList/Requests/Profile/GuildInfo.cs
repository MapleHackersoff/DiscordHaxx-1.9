using Discord;
using Newtonsoft.Json;

namespace DiscordHaxx
{
    public class GuildInfo
    {
        public GuildInfo(BaseGuild guild)
        {
            Name = guild.Name;
            _id = guild.Id.ToString();
        }


        [JsonProperty("name")]
        public string Name { get; private set; }


        [JsonProperty("id")]
        private readonly string _id;

        public ulong Id
        {
            get { return _id == null ? 0 : ulong.Parse(_id); }
        }
    }
}
