using Newtonsoft.Json;

namespace DiscordHaxx
{
    public class NameId
    {
        public NameId(string name, ulong id)
        {
            _name = name;
            _id = id.ToString();
        }



#pragma warning disable IDE0052
        [JsonProperty("name")]
        private readonly string _name;


        [JsonProperty("id")]
        private readonly string _id;
#pragma warning restore IDE0052
    }
}
