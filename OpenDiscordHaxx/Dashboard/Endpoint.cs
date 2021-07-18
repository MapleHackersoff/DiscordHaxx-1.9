using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace DiscordHaxx
{
    public class DashboardEndpoint : WebSocketBehavior
    {
        protected override void OnOpen()
        {
            Send(new DashboardRequest<StatusUpdate>());
            Send(new DashboardRequest<OverlookUpdate>());
            Send(new DashboardRequest<AttacksUpdate>());
        }


        protected override void OnMessage(MessageEventArgs e)
        {
            JObject obj = JsonConvert.DeserializeObject<JObject>(e.Data);

            switch (obj.GetValue("op").ToObject<DashboardOpcode>())
            {
                case DashboardOpcode.KillAttack:
                    foreach (var attack in Server.OngoingAttacks)
                    {
                        if (attack.Id == obj.ToObject<AttackKillRequest>().Id)
                        {
                            attack.Bot.ShouldStop = true;
                            break;
                        }
                    }
                    break;
            }
        }


        public static void Broadcast<T>() where T : DashboardInnerRequest, new()
        {
            SocketServer.Broadcast("/dashboard", new DashboardRequest<T>());
        }
    }
}