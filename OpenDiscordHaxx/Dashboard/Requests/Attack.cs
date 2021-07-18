using System;
using Newtonsoft.Json;

namespace DiscordHaxx
{
    public class Attack
    {
        private static int _currentAttackId = 0;

        [JsonIgnore]
        public RaidBot Bot { get; private set; }

        public Attack(RaidBot bot)
        {
            Id = _currentAttackId;
            _currentAttackId++;

            Bot = bot;
        }


        [JsonProperty("type")]
        public string Type { get; set; }


        [JsonProperty("bots")]
        public int Bots { get; set; }


        [JsonProperty("threads")]
        public int Threads
        {
            get { return Bot.Threads; }
        }


        [JsonProperty("id")]
        public int Id { get; private set; }
    }
}
