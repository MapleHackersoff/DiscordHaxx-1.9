using Newtonsoft.Json;

namespace DiscordHaxx
{
    public class StatusUpdate : DashboardInnerRequest
    {
        public StatusUpdate() : base(DashboardOpcode.StatusUpdate)
        {
            _status = Server.ServerStatus;
        }


        [JsonProperty("status")]
#pragma warning disable IDE0052
        private readonly string _status;
#pragma warning restore IDE0052
    }
}
