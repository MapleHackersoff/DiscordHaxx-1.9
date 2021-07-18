using Newtonsoft.Json;
using System.Linq;
using WebSocketSharp.Server;
using Discord;
using Newtonsoft.Json.Linq;
using System;
using Discord.Gateway;
using System.Collections.Generic;

namespace DiscordHaxx
{
    public class BotListEndpoint : WebSocketBehavior
    {
        protected override void OnOpen()
        {
            Send(new ListRequest(ListAction.Add, Server.Bots));
        }


        protected override void OnMessage(WebSocketSharp.MessageEventArgs e)
        {
            JObject obj = JsonConvert.DeserializeObject<JObject>(e.Data);

            switch (obj.GetValue("op").ToObject<ListOpcode>())
            {
                case ListOpcode.Token:
                    TokenRequest tokenReq = obj.ToObject<TokenRequest>();
                    DiscordClient client = Server.Bots.First(c => c.Client.User.Id == tokenReq.Id);
                    tokenReq.Token = client.Token;
                    tokenReq.At = client.User.ToString();

                    Send(tokenReq);
                    break;
                case ListOpcode.BotModify:
                    ModRequest modReq = obj.ToObject<ModRequest>();

                    if (modReq.SetAll)
                    {
                        foreach (var bot in new List<RaidBotClient>(Server.Bots))
                            ModifyUser(bot, modReq);
                    }
                    else
                    {
                        RaidBotClient bot = Server.Bots.First(c => c.Client.User.Id == modReq.Id);

                        ModResponse resp = new ModResponse { At = bot.Client.User.ToString() };
                        resp.Success = ModifyUser(bot, modReq);

                        Send(resp);
                    }
                    break;
                case ListOpcode.BotInfo:
                    BotInfoRequest infoReq = JsonConvert.DeserializeObject<BotInfoRequest>(e.Data);

                    SocketServer.Broadcast("/list", 
                                      BotInfo.FromClient(Server.Bots.First(b => b.Client.User.Id == infoReq.Id), infoReq.GetGuildsAndFriends));
                    break;
            }
        }


        private static bool ModifyUser(RaidBotClient client, ModRequest req)
        {
            try
            {
                if (client.Client.User.Hypesquad != req.Hypesquad)
                    client.Client.User.SetHypesquad(req.Hypesquad);

                if (req.Avatar != null)
                    client.Client.User.ChangeProfile(new UserProfile() { Avatar = req.Avatar });

                if (!client.SocketClient)
                    client.Client.User.Update();

                if (req.Status != "Unset")
                {
                    UserStatus status = (UserStatus)Enum.Parse(typeof(UserStatus), req.Status.Replace(" ", ""), true);

                    ((DiscordSocketClient)client.Client).SetStatus(status);
                }

                UpdateList(ListAction.Update, client);

                return true;
            }
            catch
            {
                return false;
            }
        }


        public static void UpdateList(ListAction action, RaidBotClient bot)
        {
            UpdateList(action, new List<RaidBotClient>() { bot });
        }


        public static void UpdateList(ListAction action, List<RaidBotClient> bots)
        {
            SocketServer.Broadcast("/list", new ListRequest(action, bots));
        }
    }
}
