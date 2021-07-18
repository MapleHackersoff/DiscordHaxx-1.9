using Newtonsoft.Json;

namespace DiscordHaxx
{
    public class BotRequest
    {
        public BotRequest(ListOpcode op)
        {
            Opcode = op;
        }


        [JsonProperty("op")]
        public ListOpcode Opcode { get; set; }


        public static implicit operator string(BotRequest instance)
        {
            return JsonConvert.SerializeObject(instance);
        }
    }
}
