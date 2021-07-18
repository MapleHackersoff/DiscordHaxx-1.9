using System.Collections.Generic;
using Discord;
using Discord.Gateway;

namespace DiscordHaxx
{
    public class RaidBotClient
    {
        public List<Guild> Guilds { get; set; }
        public List<Relationship> Relationships { get; set; }
        public DiscordClient Client { get; private set; }
        public bool SocketClient { get; private set; }

        public RaidBotClient(DiscordClient client)
        {
            Client = client;
        }


        public RaidBotClient(DiscordSocketClient client)
        {
            Client = client;
            SocketClient = true;
            client.OnJoinedGuild += Client_OnJoinedGuild;
            client.OnLeftGuild += Client_OnLeftGuild;
            client.OnUserUpdated += Client_OnUserUpdated;
            client.OnRelationshipAdded += Client_OnRelationshipAdded;
            client.OnRelationshipRemoved += Client_OnRelationshipRemoved;
        }


        private void Client_OnJoinedGuild(DiscordSocketClient client, GuildEventArgs args)
        {
            Guilds.Add(args.Guild);
        }


        private void Client_OnLeftGuild(DiscordSocketClient client, GuildEventArgs args)
        {
            Guilds.Remove(args.Guild);
        }


        private void Client_OnUserUpdated(DiscordSocketClient client, UserEventArgs args)
        {
            if (args.User.Id == client.User.Id)
                BotListEndpoint.UpdateList(ListAction.Update, new RaidBotClient(client));
        }


        private void Client_OnRelationshipAdded(DiscordSocketClient client, RelationshipEventArgs args)
        {
            Relationships.Add(args.Relationship);
        }


        private void Client_OnRelationshipRemoved(object sender, RelationshipEventArgs args)
        {
            Relationships.Remove(args.Relationship);
        }


        public static implicit operator DiscordClient(RaidBotClient instance)
        {
            return instance.Client;
        }


        public override string ToString()
        {
            return Client.User.ToString();
        }
    }
}
