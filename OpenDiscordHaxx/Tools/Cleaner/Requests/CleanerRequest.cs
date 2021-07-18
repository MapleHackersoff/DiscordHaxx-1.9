using Newtonsoft.Json;

namespace DiscordHaxx
{
    public class CleanerRequest
    {
        public CleanerRequest(CleanerOpcode op)
        {
            Opcode = op;
        }


        [JsonProperty("op")]
        public CleanerOpcode Opcode { get; private set; }
    }
}
