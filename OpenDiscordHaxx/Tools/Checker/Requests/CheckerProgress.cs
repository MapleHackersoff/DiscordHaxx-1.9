using Newtonsoft.Json;

namespace DiscordHaxx
{
    public class CheckerProgress
    {
        [JsonProperty("valid")]
        public int Valid { get; set; }


        [JsonProperty("invalid")]
        public int Invalid { get; set; }


        [JsonProperty("total")]
        public int Total { get; set; }
    }
}
