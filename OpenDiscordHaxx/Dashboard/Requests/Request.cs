using Newtonsoft.Json;

namespace DiscordHaxx
{
    public class DashboardRequest<T> where T : DashboardInnerRequest, new()
    {
        [JsonProperty("data")]
        public T Data { get; set; }


        [JsonProperty("op")]
        public DashboardOpcode Opcode
        {
            get { return Data.Opcode; }
        }


        public DashboardRequest()
        {
            Data = new T();
        }


        public static implicit operator string(DashboardRequest<T> instance)
        {
            return JsonConvert.SerializeObject(instance);
        }
    }
}
