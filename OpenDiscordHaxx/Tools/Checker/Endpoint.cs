using Newtonsoft.Json;
using WebSocketSharp.Server;

namespace DiscordHaxx
{
    public class CheckerEndpoint : WebSocketBehavior
    {
        private static Checker _checker;


        protected override void OnOpen()
        {
            if (_checker == null || _checker.Finished)
            {
                _checker = new Checker();
                _checker.StartAsync();
            }
            else
                Send(JsonConvert.SerializeObject(new CheckerResumeRequest(_checker.Progress)));
        }
    }
}
