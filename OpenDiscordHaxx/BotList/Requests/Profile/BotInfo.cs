using Discord;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DiscordHaxx
{
    public class BotInfo : BotRequest
    {
        public BotInfo() : base(ListOpcode.BotInfo)
        {
            Badges = new List<string>();
        }

        [JsonProperty("at")]
        public string At { get; private set; }


        [JsonProperty("avatar_id")]
        public string AvatarId { get; private set; }


        [JsonProperty("id")]
        public string Id { get; set; }


        [JsonProperty("badges")]
        public List<string> Badges { get; private set; }


        [JsonProperty("verification")]
        public string Verification { get; set; }


        [JsonProperty("guilds")]
        public List<GuildInfo> Guilds { get; private set; }


        [JsonProperty("friends")]
        public List<FriendInfo> Friends { get; private set; }


        [JsonProperty("gateway")]
        public bool Gateway { get; private set; }


        public static BotInfo FromClient(RaidBotClient bot, bool getGuildsAndFriends)
        {
            BotInfo info = new BotInfo
            {
                At = bot.Client.User.ToString(),
                Id = bot.Client.User.Id.ToString(),
                AvatarId = bot.Client.User.AvatarId,
                Gateway = bot.SocketClient
            };

            if (bot.Client.User.TwoFactorAuth)
                info.Verification = "Phone verified";
            else if (bot.Client.User.EmailVerified)
                info.Verification = "Email verified";
            else
                info.Verification = "None or locked";

            foreach (Enum value in Enum.GetValues(typeof(DiscordBadge)))
            {
                if (value.ToString() == "LocalUser" || value.ToString() == "None")
                    continue;

                if (bot.Client.User.Badges.HasFlag(value))
                    info.Badges.Add(value.ToString());
            }

            if (bot.Client.User.Nitro > NitroType.None)
                info.Badges.Add("Nitro");

            if (getGuildsAndFriends)
            {
                info.Guilds = new List<GuildInfo>();
                info.Friends = new List<FriendInfo>();

                if (bot.SocketClient)
                {
                    foreach (var guild in bot.Guilds)
                        info.Guilds.Add(new GuildInfo(guild));
                }
                else
                {
                    foreach (var guild in bot.Client.GetGuilds())
                        info.Guilds.Add(new GuildInfo(guild));
                }

                foreach (var relationship in bot.SocketClient ? bot.Relationships : bot.Client.GetRelationships())
                {
                    if (relationship.Type == RelationshipType.Friends)
                        info.Friends.Add(new FriendInfo(relationship));
                }
            }

            return info;
        }
    }
}
