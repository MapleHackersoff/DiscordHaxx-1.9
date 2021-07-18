using Newtonsoft.Json;

namespace DiscordHaxx
{
    public class Config
    {
        [JsonProperty("enable_gateway")]
        public bool EnableGateway { get; private set; }

        
        [JsonProperty("gateway_cap")]
        public int GatewayCap { get; private set; }
    }
}
