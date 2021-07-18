using Discord;
using Discord.Gateway;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DiscordHaxx
{
    public class AccountList
    {
        public List<RaidBotClient> Accounts { get; private set; }
        private Config _config;
        private bool _reloaderRunning;
        private bool _tokensLoading;

        public AccountList()
        {
            Accounts = new List<RaidBotClient>();
        }


        public async void LoadAsync()
        {
            try
            {
                _config = JsonConvert.DeserializeObject<Config>(File.ReadAllText("Config.json"));
            }
            catch
            {
                _config = new Config();
            }

            await Task.Run(() =>
            {
                BotListEndpoint.UpdateList(ListAction.Remove, Accounts);
                Accounts.Clear();
                Server.ServerStatus = "Loading bots";
                _tokensLoading = true;


                string[] tokens = File.Exists("Tokens.txt") 
                                        ? File.ReadAllLines("Tokens.txt") : new string[] { };

                foreach (var token in tokens.Distinct())
                {
                    if (string.IsNullOrWhiteSpace(token))
                        continue;

                    try
                    {
                        RaidBotClient client = null;

                        if (Accounts.Count <= _config.GatewayCap && _config.EnableGateway)
                        {
                            DiscordSocketClient sClient = new DiscordSocketClient();
                            sClient.OnLoggedIn += Client_OnLoggedIn;
                            sClient.Login(token);
                            client = new RaidBotClient(sClient);
                        }
                        else
                            client = new RaidBotClient(new DiscordClient(token));

                        Accounts.Add(client);
                        BotListEndpoint.UpdateList(ListAction.Add, client);
                    }
                    catch (DiscordHttpException) { }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Unknown error when loading account:\n{e}");
                    }
                }


                var bruh = Accounts.GroupBy(bot => bot.Client.User.Id);
                List<RaidBotClient> removedAccounts = new List<RaidBotClient>();
                foreach (var ok in bruh)
                    removedAccounts.AddRange(ok.ToList().Skip(1));

                Accounts = bruh.Select(group => group.First()).ToList();


                if (removedAccounts.Count > 0)
                {
                    foreach (var bot in new List<RaidBotClient>(removedAccounts))
                    {
                        if (Accounts.Contains(bot))
                            removedAccounts.RemoveAt(removedAccounts.IndexOf(bot));
                    }

                    BotListEndpoint.UpdateList(ListAction.Remove, removedAccounts);
                }


                if (Accounts.Count < tokens.Length)
                {
                    StringBuilder builder = new StringBuilder();
                    foreach (var bot in Accounts)
                        builder.AppendLine(bot.Client.Token);

                    File.WriteAllText("Tokens-valid.txt", builder.ToString());
                }


                Server.ServerStatus = "Ready";
                _tokensLoading = false;

                if (!_reloaderRunning)
                    StartAutoReloaderAsync();
            });
        }

        private void Client_OnLoggedIn(DiscordSocketClient client, LoginEventArgs args)
        {
            RaidBotClient raidClient = Accounts.First(acc => acc.Client.User.Id == client.User.Id);
            raidClient.Guilds = args.Guilds.Cast<Guild>().ToList(); 
            raidClient.Relationships = args.Relationships.ToList();
        }

        private async void StartAutoReloaderAsync()
        {
            await Task.Run(() =>
            {
                _reloaderRunning = true;

                string currentContent = null;

                if (File.Exists("Tokens.txt"))
                     currentContent = File.ReadAllText("Tokens.txt");

                while (true)
                {
                    if (File.Exists("Tokens.txt"))
                    {
                        string content = File.ReadAllText("Tokens.txt");

                        if (currentContent != content && !_tokensLoading)
                        {
                            LoadAsync();

                            currentContent = content;
                        }

                        Thread.Sleep(1500);
                    }
                }
            });
        }
    }
}
