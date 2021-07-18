using Discord;
using Newtonsoft.Json;

namespace DiscordHaxx
{
    public class BasicBotInfo
    {
        [JsonProperty("at")]
        public string At { get; set; }


        [JsonProperty("id")]
        public string Id { get; set; }


        [JsonProperty("hypesquad")]
        public string Hypesquad { get; set; }


        [JsonProperty("verification")]
        public string Verification { get; set; }


        public static BasicBotInfo FromClient(RaidBotClient client)
        {
            BasicBotInfo bot = new BasicBotInfo()
            {
                At = client.Client.User.ToString(),
                Id = client.Client.User.Id.ToString(),
                Hypesquad = client.Client.User.Hypesquad.ToString()
            };

            if (client.Client.User.TwoFactorAuth)
                bot.Verification = "Phone verified";
            else if (client.Client.User.EmailVerified)
                bot.Verification = "Email verified";
            else
                bot.Verification = "None or locked";

            return bot;
        }
    }
}
