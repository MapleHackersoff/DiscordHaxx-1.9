using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace DiscordHaxx
{
    public static class Server
    {
        private static string _serverStatus;
        public static string ServerStatus
        {
            get { return _serverStatus; }
            set
            {
                _serverStatus = value;

                DashboardEndpoint.Broadcast<StatusUpdate>();
            }
        }


        public static AccountList AccountList = new AccountList();
        public static List<RaidBotClient> Bots
        {
            get { return AccountList.Accounts; }
        }

        public static async void StartAccountBroadcasterAsync()
        {
            await Task.Run(() =>
            {
                int previousAmount = 0;

                while (true)
                {
                    if (Bots.Count != previousAmount)
                    {
                        previousAmount = Bots.Count;

                        DashboardEndpoint.Broadcast<OverlookUpdate>();
                    }

                    Thread.Sleep(1100);
                }
            });
        }


        public static ObservableCollection<Attack> OngoingAttacks = new ObservableCollection<Attack>();
    }
}
