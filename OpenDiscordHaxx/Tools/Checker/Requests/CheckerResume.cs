using Newtonsoft.Json;

namespace DiscordHaxx
{
    class CheckerResumeRequest : CheckerRequest
    {
        public CheckerResumeRequest(CheckerProgress progress) : base(CheckerOpcode.Resume)
        {
            Progress = progress;
        }


        [JsonProperty("progress")]
        public CheckerProgress Progress { get; private set; }
    }
}
