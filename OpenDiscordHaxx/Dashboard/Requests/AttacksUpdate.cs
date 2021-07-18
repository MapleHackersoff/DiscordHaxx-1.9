using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace DiscordHaxx
{
    public class AttacksUpdate : DashboardInnerRequest
    {
        public AttacksUpdate() : base(DashboardOpcode.AttacksUpdate)
        {
            _attacks = Server.OngoingAttacks;
        }

        [JsonProperty("attacks")]
#pragma warning disable IDE0052
        private readonly ObservableCollection<Attack> _attacks;
#pragma warning restore IDE0052
    }
}
