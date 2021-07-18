using Discord;
using Newtonsoft.Json;

namespace DiscordHaxx
{
    public class FriendInfo
    {
        public FriendInfo(Relationship relationship)
        {
            At = relationship.User.ToString();
            _id = relationship.User.Id.ToString();
        }


        [JsonProperty("at")]
        public string At { get; private set; }


        [JsonProperty("id")]
        private readonly string _id;

        public ulong Id
        {
            get { return _id == null ? 0 : ulong.Parse(_id); }
        }
    }
}
