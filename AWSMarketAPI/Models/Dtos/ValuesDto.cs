using Newtonsoft.Json;
using System.Numerics;

namespace AWSMarketAPI.Models.Dtos
{
    public class ValuesDto
    {
        [JsonProperty("1. open")]
        public decimal Open { get; set; }
        [JsonProperty("2. high")]
        public decimal High { get; set; }

        [JsonProperty("3. low")]
        public decimal Low { get; set; }

        [JsonProperty("4. close")]
        public decimal Close { get; set; }

        [JsonProperty("5. volume")]
        public int volume { get; set; }
    }
}