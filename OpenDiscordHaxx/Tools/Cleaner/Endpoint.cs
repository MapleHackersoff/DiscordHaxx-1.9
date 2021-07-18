using Newtonsoft.Json;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace DiscordHaxx
{
    public class CleanerEndpoint : WebSocketBehavior
    {
        private static Cleaner _cleaner;


        protected override void OnOpen()
        {
            if (_cleaner != null)
            {
                if (!_cleaner.Finished)
                    Send(JsonConvert.SerializeObject(new CleanerRequest(CleanerOpcode.CleanerResume)));
            }
        }


        protected override void OnMessage(MessageEventArgs e)
        {
            CleanOptions options = JsonConvert.DeserializeObject<CleanOptions>(e.Data);

            if (options.Opcode == CleanerOpcode.StartCleaner)
            {
                if (_cleaner == null || _cleaner.Finished)
                {
                    _cleaner = new Cleaner(options);
                    _cleaner.StartAsync();
                }
            }
        }
    }
}
