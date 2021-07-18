using Newtonsoft.Json;

namespace DiscordHaxx
{
    public class OverlookUpdate : DashboardInnerRequest
    {
        public OverlookUpdate() : base(DashboardOpcode.OverlookUpdate)
        {
            _accounts = Server.Bots.Count;
            _attacks = Server.OngoingAttacks.Count;
        }


#pragma warning disable IDE0052
        [JsonProperty("accounts")]
        private readonly int _accounts;


        [JsonProperty("attacks")]
        private readonly int _attacks;
#pragma warning restore IDE0052
    }
}
