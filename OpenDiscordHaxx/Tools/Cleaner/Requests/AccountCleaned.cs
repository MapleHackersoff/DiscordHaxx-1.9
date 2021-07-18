using Discord;
using Newtonsoft.Json;

namespace DiscordHaxx
{
    public class AccountCleanedRequest : CleanerRequest
    {
        public AccountCleanedRequest(DiscordClient client) : base(CleanerOpcode.AccountCleaned)
        {
            At = client.User.ToString();
        }


        [JsonProperty("at")]
        public string At { get; private set; }
    }
}
