using Newtonsoft.Json;
using System;

namespace AWSMarketAPI.Models.Dtos
{
    public class MetaDataDto
    {
        [JsonProperty("1. Information")]
        public string Information { get; set; }
        [JsonProperty("2. Symbol")]
        public string Symbol { get; set; }

        [JsonProperty("3. Last Refreshed")]
        public DateTime LastRefreshed { get; set; }

        [JsonProperty("4. Output Size")]
        public string Output { get; set; }

        [JsonProperty("5. Time Zone")]
        public string TimeZone { get; set; }

    }
}