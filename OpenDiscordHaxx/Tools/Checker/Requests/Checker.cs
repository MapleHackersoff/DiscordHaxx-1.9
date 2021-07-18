using Newtonsoft.Json;

namespace DiscordHaxx
{
    public class CheckerRequest
    {
        public CheckerRequest(CheckerOpcode op)
        {
            Opcode = op;
        }


        [JsonProperty("op")]
        public CheckerOpcode Opcode { get; private set; }


        public static implicit operator string(CheckerRequest instance)
        {
            return JsonConvert.SerializeObject(instance);
        }
    }
}
