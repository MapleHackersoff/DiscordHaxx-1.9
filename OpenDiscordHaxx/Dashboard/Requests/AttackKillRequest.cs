using Newtonsoft.Json;

namespace DiscordHaxx
{
    public class AttackKillRequest : DashboardInnerRequest
    {
        public AttackKillRequest() : base(DashboardOpcode.KillAttack)
        { }


        [JsonProperty("id")]
        public int Id { get; private set; }
    }
}
