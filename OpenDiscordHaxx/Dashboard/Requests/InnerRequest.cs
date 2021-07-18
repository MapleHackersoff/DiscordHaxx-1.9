using Newtonsoft.Json;

namespace DiscordHaxx
{
    public class DashboardInnerRequest
    {
        public DashboardInnerRequest(DashboardOpcode op)
        {
            Opcode = op;
        }


        [JsonIgnore]
        public DashboardOpcode Opcode { get; private set; }
    }
}
