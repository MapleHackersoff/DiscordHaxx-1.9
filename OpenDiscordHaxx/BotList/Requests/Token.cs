using Newtonsoft.Json;

namespace DiscordHaxx
{
    class TokenRequest : BotRequest
    {
        public TokenRequest() : base(ListOpcode.Token)
        { }



        [JsonProperty("id")]
#pragma warning disable CS0649
        private readonly string _id;
#pragma warning restore CS0649

        public ulong Id
        {
            get { return ulong.Parse(_id); }
        }


        [JsonProperty("token")]
        public string Token { get; set; }


        [JsonProperty("at")]
        public string At { get; set; }
    }
}
