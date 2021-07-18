using Newtonsoft.Json;

namespace DiscordHaxx
{
    public class CleanOptions : CleanerRequest
    {
        public CleanOptions() : base(CleanerOpcode.StartCleaner)
        { }

        [JsonProperty("remove_guilds")]
        public bool RemoveGuilds { get; private set; }


        [JsonProperty("remove_relationships")]
        public bool RemoveRelationships { get; private set; }


        [JsonProperty("remove_private_channels")]
        public bool RemoveDMs { get; private set; }


        [JsonProperty("reset_profile")]
        public bool ResetProfile { get; private set; }
    }
}
