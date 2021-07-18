using Newtonsoft.Json;

namespace DiscordHaxx
{
    public class BotStartSuccess
    {
        [JsonProperty("succeeded")]
        public bool Succeeded { get; set; }


        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
