using Newtonsoft.Json;

namespace DiscordHaxx
{
    public class CheckerStartedRequest : CheckerRequest
    {
        public CheckerStartedRequest() : base(CheckerOpcode.Started)
        {
            _progress = new CheckerProgress() { Total = Server.Bots.Count };
        }


        [JsonProperty("progress")]
#pragma warning disable IDE0044, IDE0052
        private CheckerProgress _progress;
#pragma warning restore IDE0044, IDE0052
    }
}
