using Discord;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace DiscordHaxx
{
    public class Checker
    {
        public CheckerProgress Progress { get; private set; }
        public bool Finished { get; private set; }
        private readonly string _checkDir;


        public Checker()
        {
            Progress = new CheckerProgress
            { Total = Server.Bots.Count };

            int i = 1;
            while (true)
            {
                _checkDir = "Checker-results " + i;

                if (!Directory.Exists(_checkDir))
                {
                    Directory.CreateDirectory(_checkDir);

                    break;
                }
                else
                    i++;
            }
        }


        public async void StartAsync()
        {
            await Task.Run(() =>
            {
                if (Server.Bots.Count == 0)
                {
                    SocketServer.Broadcast("/checker", new CheckerErrorRequest("notokens"));
                    Finished = true;
                    return;
                }

                SocketServer.Broadcast("/checker", new CheckerStartedRequest());
                List<RaidBotClient> uncheckedClients = new List<RaidBotClient>(Server.Bots);
                foreach (var client in new List<RaidBotClient>(uncheckedClients))
                {
                    BotCheckedRequest req = new BotCheckedRequest(client);

                    try
                    {
                        client.Client.JoinGuild("a");
                    }
                    catch (DiscordHttpException e)
                    {
                        if (e.Code == DiscordError.UnknownInvite || e.Code == DiscordError.MaximumGuilds)
                            req.Valid = true;
                        else
                        {
                            if (e.Code == DiscordError.AccountUnverified)
                                File.AppendAllText(_checkDir + "/Locked.txt", client.Client.Token + "\n");
                            else
                                File.AppendAllText(_checkDir + "/Invalid.txt", client.Client.Token + "\n");
                        }
                    }
                    catch (JsonReaderException)
                    {
                        SocketServer.Broadcast("/checker", new CheckerErrorRequest("ratelimit"));

                        List<string> uncheckedTokens = new List<string>();
                        foreach (var acc in uncheckedClients)
                            uncheckedTokens.Add(acc.Client.Token);

                        File.WriteAllText(_checkDir + "/Unchecked.txt", string.Join("\n", uncheckedTokens));
                        Finished = true;
                        break;
                    }

                    uncheckedClients.Remove(client);

                    if (req.Valid)
                    {
                        File.AppendAllText(_checkDir + "/Valid.txt", client.Client.Token + "\n");

                        Progress.Valid++;
                    }
                    else
                    {
                        Server.Bots.Remove(client);
                        SocketServer.Broadcast("/list", new ListRequest(ListAction.Remove, client));
                        Progress.Invalid++;
                    }

                    req.Progress = Progress;

                    SocketServer.Broadcast("/checker", req);
                }

                SocketServer.Broadcast("/checker", new CheckerRequest(CheckerOpcode.Done));

                Finished = true;
            });
        }
    }
}
