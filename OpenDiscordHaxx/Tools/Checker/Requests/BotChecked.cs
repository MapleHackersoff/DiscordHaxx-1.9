using Discord;
using Newtonsoft.Json;

namespace DiscordHaxx
{
    public class BotCheckedRequest : CheckerRequest
    {
        public BotCheckedRequest(RaidBotClient client) : base(CheckerOpcode.BotChecked)
        {
            Bot = BasicBotInfo.FromClient(client);
            Progress = new CheckerProgress();
        }


        [JsonProperty("valid")]
        public bool Valid { get; set; }


        [JsonProperty("bot")]
        public BasicBotInfo Bot { get; set; }


        [JsonProperty("progress")]
        public CheckerProgress Progress { get; set; }
    }
}
