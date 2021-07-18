using Discord;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Threading.Tasks;
using WebSocketSharp.Server;

namespace DiscordHaxx
{
    public class ReconEndpoint : WebSocketBehavior
    {
        private static int _nextId;
        private int _id;


        protected override void OnOpen()
        {
            _id = _nextId;
            _nextId++;

            Send(JsonConvert.SerializeObject(new ReconRequest(_id, ReconOpcode.Id)));
        }


        protected override void OnMessage(WebSocketSharp.MessageEventArgs e)
        {
            JObject obj = JsonConvert.DeserializeObject<JObject>(e.Data);

            switch (obj.GetValue("op").ToObject<ReconOpcode>())
            {
                case ReconOpcode.StartRecon:
                    Task.Run(() =>
                    {
                        var req = obj.ToObject<StartReconRequest>();

                        int bots = 0;
                        Guild guild = null;

                        foreach (var bot in Server.Bots)
                        {
                            try
                            {
                                if (bot.SocketClient)
                                {
                                    if (guild == null)
                                    {
                                        guild = bot.Client.GetGuild(req.GuildId);

                                        bots++;
                                    }
                                    else
                                    {
                                        if (bot.Guilds.Where(g => g.Id == req.GuildId).Count() > 0)
                                            bots++;
                                    }
                                }
                                else
                                {
                                    guild = bot.Client.GetGuild(req.GuildId);

                                    bots++;
                                }
                            }
                            catch { }
                        }


                        if (guild == null)
                        {
                            SocketServer.Broadcast("/recon", new ReconRequest(_id, ReconOpcode.ReconFailed));

                            return;
                        }


                        ServerRecon recon = new ServerRecon(_id)
                        {
                            Name = guild.Name,
                            Description = guild.Description ?? "No description",
                            Region = guild.Region,
                            VerificationLevel = guild.VerificationLevel.ToString(),
                            VanityInvite = guild.VanityInvite ?? "None",
                            BotsInGuild = $"{bots.ToString()}/{Server.Bots.Count}"
                        };

                        foreach (var role in guild.Roles.Where(r => r.Mentionable))
                            recon.Roles.Add(new NameId(role.Name, role.Id));

                        foreach (var emoji in guild.Emojis.Distinct())
                            recon.Emojis.Add(new NameId(emoji.Name, emoji.Id.Value));

                        SocketServer.Broadcast("/recon", recon);
                    });
                    break;
            }
        }
    }
}
