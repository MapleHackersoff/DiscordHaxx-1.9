using Newtonsoft.Json;

namespace DiscordHaxx
{
    public class ReconRequest
    {
        public ReconRequest(int id, ReconOpcode op)
        {
            Opcode = op;
            Id = id;
        }


        [JsonProperty("op")]
        public ReconOpcode Opcode { get; private set; }


        [JsonProperty("id")]
        public int Id { get; private set; }
    }
}
