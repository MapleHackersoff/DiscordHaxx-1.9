using Newtonsoft.Json;
using System.Net;
using WebSocketSharp.Server;

namespace DiscordHaxx
{
    public static class SocketServer
    {
        private static WebSocketServer _server;

        public static void Start()
        {
            _server = new WebSocketServer(IPAddress.Parse("127.0.0.1"), 420);
            _server.AddWebSocketService<DashboardEndpoint>("/dashboard");
            _server.AddWebSocketService<BotListEndpoint>("/list");
            _server.AddWebSocketService<RaidBotEndpoint>("/raid");
            _server.AddWebSocketService<CheckerEndpoint>("/checker");
            _server.AddWebSocketService<CleanerEndpoint>("/cleaner");
            _server.AddWebSocketService<ReconEndpoint>("/recon");
            _server.Start();
        }


        public static void Broadcast<T>(string endpoint, T request)
        {
            if (_server.IsListening)
            {
                _server.WebSocketServices[endpoint].Sessions
                                .Broadcast(JsonConvert.SerializeObject(request));
            }
        }
    }
}
