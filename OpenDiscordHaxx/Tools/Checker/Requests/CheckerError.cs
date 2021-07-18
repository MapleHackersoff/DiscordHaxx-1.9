using Newtonsoft.Json;

namespace DiscordHaxx
{
    public class CheckerErrorRequest : CheckerRequest
    {
        [JsonProperty("error")]
#pragma warning disable IDE0044, IDE0052
        private string _error;
#pragma warning restore IDE0044, IDE0052


        public CheckerErrorRequest(string error) : base(CheckerOpcode.Error)
        {
            _error = error;
        }
    }
}
